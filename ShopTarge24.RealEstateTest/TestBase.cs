using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using ShopTarge24.RealEstateTest.Macros;
using ShopTARge24.ApplicationServices.Services;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Extensions.Hosting;
using ShopTarge24.RealEstateTest.Mock;

namespace ShopTarge24.RealEstateTest
{
    public abstract class TestBase
    {
        protected IServiceProvider serviceProvider {  get; set; }
        protected TestBase()
        {
            var services = new ServiceCollection();
            SetupServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        public virtual void SetupServices(IServiceCollection services)
        {
            services.AddScoped<IRealEstateServices, RealEstateServices>();
            services.AddScoped<IFileServices, FileServices>();
            services.AddScoped<IHostEnvironment, MockIHostEnviroment>();

            services.AddDbContext<ShopTARge24Context>(x =>
            {
                x.UseInMemoryDatabase("TEST");
                x.ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            RegisterMacros(services);
        }

        public void Dispose()
        {

        }

        protected T Svc<T>()
        {
            // Resolve service from the service provider
            // https://rubenmamo.com/articles/solved-azure-functions-in-azure-container-apps-unable-to-resolve-service-for-type-microsoft-aspnetcore-hosting-ihostingenvironment-while-attempting-to-activate-microsoft-aspnetcore-hosting-de
            return serviceProvider.GetService<T>();
        }

        private void RegisterMacros(IServiceCollection services)
        {
            var macroBaseType = typeof(IMacros);

            var macros = macroBaseType.Assembly.GetTypes()
                .Where(t => macroBaseType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var macro in macros)
            {
                services.AddSingleton(macro);
            }
        }
    }
}
