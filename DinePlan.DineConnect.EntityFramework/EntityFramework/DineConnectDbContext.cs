using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using DinePlan.DineConnect.Authorization.Roles;
using DinePlan.DineConnect.Authorization.Users;
using DinePlan.DineConnect.Chat;
using DinePlan.DineConnect.Friendships;
using DinePlan.DineConnect.MultiTenancy;
using DinePlan.DineConnect.Storage;
using DinePlan.DineConnect.House;

namespace DinePlan.DineConnect.EntityFramework
{
    public class DineConnectDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        /* Define an IDbSet for each entity of the application */

        public virtual IDbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual IDbSet<Friendship> Friendships { get; set; }

        public virtual IDbSet<ChatMessage> ChatMessages { get; set; }

        public virtual IDbSet<Supplier> Suppliers { get; set; }

        /* Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         * But it may cause problems when working Migrate.exe of EF. ABP works either way.         * 
         */
        public DineConnectDbContext()
            : base("Default")
        {
            
        }

        /* This constructor is used by ABP to pass connection string defined in DineConnectDataModule.PreInitialize.
         * Notice that, actually you will not directly create an instance of DineConnectDbContext since ABP automatically handles it.
         */
        public DineConnectDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /* This constructor is used in tests to pass a fake/mock connection.
         */
        public DineConnectDbContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {

        }
    }
}
