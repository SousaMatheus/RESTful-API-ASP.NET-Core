using DevMS.Business.Interfaces;
using DevMS.Business.Services;
using DevMS.Data.Context;
using DevMS.Data.Repository;

namespace DevMs.Api.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext> ();
            services.AddScoped<IFornecedorRepository, FornecedorRepository> ();
            services.AddScoped<IFornecedorService, FornecedorService> ();
            return services;
        }
    }
}
