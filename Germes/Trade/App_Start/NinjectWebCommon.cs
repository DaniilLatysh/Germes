[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Trade.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Trade.App_Start.NinjectWebCommon), "Stop")]

namespace Trade.App_Start
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using DataLayer.DAL.Interfaces;
    using DataLayer.DAL.Entities;
    using DataLayer.DAL.Repositories;
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
            string connectionName = "Connection";
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                kernel.Bind<IRepository<Order>>()
                   .To<EFOrderRepository>()
                   .WithConstructorArgument("name", connectionName);

                kernel.Bind<IRepository<Product>>()
                    .To<EFProductRepository>()
                    .WithConstructorArgument("name", connectionName);

                kernel.Bind<IRepository<Price>>()
                    .To<EFPriceRepository>()
                    .WithConstructorArgument("name", connectionName);

                kernel.Bind<IRepository<Category>>()
                    .To<EFCategoryRepository>()
                    .WithConstructorArgument("name", connectionName);

                kernel.Bind<IRepository<Client>>()
                    .To<EFClientRepository>()
                    .WithConstructorArgument("name", connectionName);

                kernel.Bind<IRepository<Status>>()
                    .To<EFStatusRepository>()
                    .WithConstructorArgument("name", connectionName);

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
        }
    }
}
