using System;
using System.Runtime.InteropServices;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
    public static class UnityExtenstions
    {
        public static IUnityContainer RegisterAsSingleton<TFrom, TTo>(this IUnityContainer container,
            params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            return container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterAsSingleton<TInterface>(this IUnityContainer container,
            TInterface instance)
        {
            return container.RegisterInstance(instance, new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer RegisterAsSingleton<TInterface>(this IUnityContainer container,
            Func<TInterface> createFunc)
        {
            return container.RegisterInstance(createFunc(), new ContainerControlledLifetimeManager());
        }
    }
}