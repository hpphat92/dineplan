using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using EntityFramework.DynamicFilters;
using DinePlan.DineConnect.Migrations.Seed.Host;
using DinePlan.DineConnect.Migrations.Seed.Tenants;

namespace DinePlan.DineConnect.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<EntityFramework.DineConnectDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DineConnect";
        }

        protected override void Seed(EntityFramework.DineConnectDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantBuilder(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases using Tenant property...
            }

            context.SaveChanges();
        }
    }
}
