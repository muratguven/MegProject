
using MegProject.Data.Repositories.RoleAction;
using MegProject.Data.Repositories.SystemActions;
using MegProject.Data.Repositories.SystemControllers;
using MegProject.Data.Repositories.Users;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MegProject.Business.Core.ControllerActionAppService;
using MegProject.Data.Repositories.Roles;
using MegProject.Data.Repositories.UserGroup;
using MegProject.Data.Repositories.UserRoles;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject.Web.Common;
using MegProject.Business.Manager.Ninject;
using MegProject.Business.Manager.RoleAppService;
using MegProject.Business.Manager.UserAppService;
using MegProject.Business.Manager.UserGroupAppService;


//[assembly: PreApplicationStartMethod(typeof(NinjectCore), "Start")]

namespace MegProject.Business.Manager.Ninject
{
   public static class NinjectCore
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
           
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
            //kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));
            //kernel.Bind(typeof (IApplicationCore)).To(typeof (ApplicationCore));
            // Generic Bind işlemi bulunanacak !!!!!!!!!!!!!!!!!!!!!!!!!!

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
