using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using System.Linq;
using System.Web.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SNSRi.Web.App_Start.UnityWebActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(SNSRi.Web.App_Start.UnityWebActivator), "Shutdown")]

namespace SNSRi.Web.App_Start
{
    /// <summary>Provides the bootstrapping for integrating Unity with ASP.NET MVC.</summary>
    public static class UnityWebActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start() 
        {
            var container = UnityConfig.GetConfiguredContainer();

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));

            UnityConfig.RegisterComponents();
            UnityConfig.RegisterTypes(container, new PerRequestLifetimeManager());
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // http://stackoverflow.com/a/30887576/578334
            var signalRContainer = new UnityContainer();
            UnityConfig.RegisterTypes(signalRContainer, new ContainerControlledLifetimeManager());
            var unityHubActivator = new UnityHubActivator(signalRContainer);
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => unityHubActivator);
            
            

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            // Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}