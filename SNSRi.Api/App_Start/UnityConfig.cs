using Microsoft.Practices.Unity;
using System.Web.Http;
using SNSRi.Web.App_Start;
using Unity.WebApi;
using System;
using SNSRi.Repository;
using SNSRi.Business;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using SNSRi.Common;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Data.Common;
using AutoMapper;

namespace SNSRi.Web
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            RegisterTypes(container, new PerRequestLifetimeManager());
        }

        public static void RegisterTypes(IUnityContainer container, LifetimeManager lifetimeManager)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            container.RegisterType<ITicketRepository, TicketRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IDeviceRepository, DeviceRepository>();
            container.RegisterType<IRoomDeviceRepository, RoomDeviceRepository>();
            container.RegisterType<IRoomRepository, RoomRepository>();
            container.RegisterType<IHSDeviceRepository, HSDeviceRepository>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IHomeSeerUnitOfWork, HomeSeerUnitOfWork>();
            container.RegisterType<IFactoryResetter, FactoryResetter>();
            container.RegisterType<IHttpClient, SNSRiHttpClient>();
            container.RegisterType<DbContext, SNSRiContext>(lifetimeManager);
            container.RegisterType<IResidentBL, ResidentBL>();
            container.RegisterType<IResidentRepository, ResidentRepository>();
            container.RegisterType<IDeviceControlRepository, DeviceControlRepository>();
            container.RegisterType<IEventRepository, EventRepository>();
            container.RegisterType<IEventMonitor, EventMonitor>();
            container.RegisterType<ITicketingUnitOfWork, TicketingUnitOfWork>();
        }

        public static void RegisterComponents()
        {
            var container = GetConfiguredContainer();
            RegisterTypes(container);
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}