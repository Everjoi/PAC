using Autofac;
using Microsoft.Extensions.Caching.Memory;
using PAC.Application.Interfaces.Repository;
using PAC.Persistant.Data.Context;
using PAC.Persistant.Repository;


namespace PAC.Persistant.ModuleContainer
{
    public class PACModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PACContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<MemoryCache>().As<IMemoryCache>().InstancePerLifetimeScope();
            builder.RegisterType<PACContext>().AsSelf().InstancePerLifetimeScope();
        }

    }
}
