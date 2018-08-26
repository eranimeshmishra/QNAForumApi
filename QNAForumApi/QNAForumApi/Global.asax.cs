using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Dapper;
using QNAForum.Business;
using QNAForum.Data;
using QNAForumApi.Core.DI;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace QNAForumApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SetupLogging();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterDI();
            ConfigureDapper();
        }

        /// <summary>
        /// Configure Dapper Settings here
        /// </summary>
        private void ConfigureDapper()
        {
            Configuration.DBSchema = "dbo";
            Configuration.UseTypeNameAsTableName = true;
        }

        /// <summary>
        /// Configure Log4net Settings here
        /// </summary>
        private void SetupLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// Configure SimpleInjector here
        /// </summary>
        private void RegisterDI()
        {
            IDIContainer diContainer = new DIContainer();
        }
    }
}
