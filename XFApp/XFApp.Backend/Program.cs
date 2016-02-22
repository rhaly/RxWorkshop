using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using GHApp.Communication;
using GHApp.Contracts.Queries;
using GHApp.Contracts.Responses;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Microsoft.Practices.Unity;
using Owin;
using Unity.WebApi;
using XFApp.Backend.Hubs;
using XFApp.Backend.Services;

namespace XFApp.Backend
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://*:22500";

            using (WebApp.Start(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.ReadLine();
            }
        }
       
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var config = new HttpConfiguration();

            var container = BuildUpContainer();

            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.DependencyResolver = new UnityDependencyResolver(container);
            GlobalHost.DependencyResolver = new SignalRUnityDependencyResolver(container);


            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
            config.EnsureInitialized(); //Nice to check for issues before first request

            app.UseWebApi(config);
        }

        private IUnityContainer BuildUpContainer()
        {
            var container =  new UnityContainer();

            container.RegisterType<IUdpClientServer, UdpClientServer>(new ContainerControlledLifetimeManager());

            var serverAddress = ConfigurationManager.AppSettings.Get("ServerAddress");
            var serverPort = int.Parse(ConfigurationManager.AppSettings.Get("ServerPort"));
            container.RegisterInstance<IChannelConfig>(ChannelNames.Server, new ChannelConfig { Address = serverAddress, Port = serverPort }, new ContainerControlledLifetimeManager());
            container.RegisterType<ICommunicationChannel, UdpCommunicationChannel>(
                ChannelNames.Server,
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(c => new UdpCommunicationChannel(c.Resolve<IUdpClientServer>(), c.Resolve<IChannelConfig>(ChannelNames.Server))));

            var clientAddress = ConfigurationManager.AppSettings.Get("ClientAddress");
            var clientPort = int.Parse(ConfigurationManager.AppSettings.Get("ClientPort"));
            container.RegisterInstance<IChannelConfig>(ChannelNames.Client, new ChannelConfig { Address = clientAddress, Port = clientPort }, new ContainerControlledLifetimeManager());
            container.RegisterType<ICommunicationChannel, UdpCommunicationChannel>(
                ChannelNames.Client,
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(c => new UdpCommunicationChannel(c.Resolve<IUdpClientServer>(), c.Resolve<IChannelConfig>(ChannelNames.Client))));

            container.RegisterType(typeof(IService<,>), typeof(Service<,>), new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(ITopic<>), typeof(Topic<>), new ContainerControlledLifetimeManager());

            var ch = container.Resolve<ICommunicationChannel>(ChannelNames.Client);
            ch.SendMessage(new UserQuery("xxx"));

            container.RegisterType(typeof(IRepoWatch), typeof(RepoWatch), new ContainerControlledLifetimeManager());
            container.RegisterType<RepoHub>(new InjectionFactory(c => new RepoHub(c.Resolve<IRepoWatch>())));


            return container;
        }
    }
}
