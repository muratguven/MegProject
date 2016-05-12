using MegProject.Business.Core.ControllerActionAppService;
using MegProject.Business.Manager.RoleAppService;
using MegProject.Business.Manager.UserAppService;
using MegProject.Business.Manager.UserGroupAppService;
using MegProject.Data.Core;
using MegProject.Data.Repositories.RoleAction;
using MegProject.Data.Repositories.Roles;
using MegProject.Data.Repositories.SystemActions;
using MegProject.Data.Repositories.SystemControllers;
using MegProject.Data.Repositories.UserGroup;
using MegProject.Data.Repositories.UserRoles;
using MegProject.Data.Repositories.Users;
using Ninject;
using Ninject.Web.Common;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MegProject.Business.Manager.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MegProject.Business.Manager.App_Start.NinjectWebCommon), "Stop")]

namespace MegProject.Business.Manager.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

   

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //Unit Of Work 
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            //User Repository and App Injection
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IUserApp>().To<UserApp>();


            //SystemController SystemActions Repository and App Injection
            kernel.Bind<ISystemControllerRepository>().To<SystemControllerRepository>();
            kernel.Bind<ISystemActionRepository>().To<SystemActionRepository>();
            kernel.Bind<IControllerActionApp>().To<ControllerActionApp>();


            //RoleAction Repository And App 
            kernel.Bind<IRoleActionRepository>().To<RoleActionRepository>();

            //UserGroup Repository And App
            kernel.Bind<IUserGroupRepository>().To<UserGroupRepository>();
            kernel.Bind<IUserGroupApp>().To<UserGroupApp>();

            //Roles Repository And App
            kernel.Bind<IRolesRepository>().To<RolesRepository>();
            kernel.Bind<IRoleApp>().To<RoleApp>();

            //UserRoles Repository 
            kernel.Bind<IUserRolesRepository>().To<UserRolesRepository>();
        }        
    }
}
