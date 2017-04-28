using System.Data.Entity;
using System.Reflection;
using Abp.Events.Bus;
using Abp.Modules;
using Castle.MicroKernel.Registration;
using DinePlan.DineConnect.EntityFramework;

namespace DinePlan.DineConnect.Migrator
{
    [DependsOn(typeof(DineConnectDataModule))]
    public class DineConnectMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<DineConnectDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(typeof(IEventBus), () =>
            {
                IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                );
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}