using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PAC.Persistant.Data.Context;
using PAC.Persistant.ModuleContainer;
using Microsoft.Extensions.Hosting;



namespace PAC.Persistant.Extension
{
    public static class ServiceCollectionExtension
    {

        public static void AddPersistenceLayer(this IServiceCollection services,IConfiguration configuration,IHostBuilder host)
        {
            services.AddDbContext(configuration);
            host.AddRepositories();
        }


        public static void AddDbContext(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<PACContext>(options =>
               options.UseSqlServer(connectionString,
                   builder => builder.MigrationsAssembly(typeof(PACContext).Assembly.FullName)));

        }


        private static void AddRepositories(this IHostBuilder builder)
        {
            builder.UseServiceProviderFactory(new AutofacServiceProviderFactory()).      
                ConfigureContainer<ContainerBuilder>( builder =>
                {
                    builder.RegisterModule(new PACModule());
                });
        }
    }
}
