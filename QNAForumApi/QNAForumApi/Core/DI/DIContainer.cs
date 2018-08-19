using System.Runtime.CompilerServices;
using System.Web.Http;
using QNAForum.Business;
using QNAForum.Data;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace QNAForumApi.Core.DI
{
    public class DIContainer : IDIContainer
    {
        private  Container container;
        public DIContainer()
        {
            container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:

            DIRegistration.RegisterDependencies(container);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }

        public TService GetInstance<TService>() where TService : class
        {
            return container.GetInstance<TService>();
        }
    }
}