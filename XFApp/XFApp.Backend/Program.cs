using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;

namespace XFApp.Backend
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:8080";

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

            //I use attribute-based routing for ApiControllers

            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
            config.EnsureInitialized(); //Nice to check for issues before first request

            app.UseWebApi(config);
        }
    }

    public class MyHub : Hub
    {
        public void Send(string name, string message)
        {
            this.Groups.Add(Context.ConnectionId, name);
            //Clients.Group()
            Clients.All.addMessage(name, message);
        }
    }
}
