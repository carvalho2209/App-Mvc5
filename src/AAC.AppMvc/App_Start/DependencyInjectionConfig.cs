using System.Reflection;
using System.Web.Mvc;
using AAC.Business.Core.Notifications;
using AAC.Business.Models.Products;
using AAC.Business.Models.Products.Services;
using AAC.Business.Models.Providers;
using AAC.Business.Models.Providers.Services;
using AAC.Infra.Data.Context;
using AAC.Infra.Data.Repository;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace AAC.AppMvc
{
    public class DependencyInjectionConfig
    {
        public static void RegisterDIConteiner()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            // LifeStyle.Singleton
            // Uma unica instancia por app

            // LifeStyle.AddScoped
            // Uma unica instancia por request

            // LifeStyle.Transient
            // cria uma nova instancia para cada injecao

            container.Register<MyDbContext>(Lifestyle.Scoped);

            container.Register<IProductRepository, ProductRepository>(Lifestyle.Scoped);
            container.Register<IProductService, ProductService>(Lifestyle.Scoped);

            container.Register<INotificator, Notificator>(Lifestyle.Scoped);

            container.Register<IProviderRepository, ProviderRepository>(Lifestyle.Scoped);
            container.Register<IProviderService, ProviderService>(Lifestyle.Scoped);

            container.Register<IAddressRepository, AddressRepository>(Lifestyle.Scoped);

            container.RegisterSingleton(() => AutoMapperConfig
                .GetMapperConfiguration()
                .CreateMapper(container.GetInstance));
        }
    }
}