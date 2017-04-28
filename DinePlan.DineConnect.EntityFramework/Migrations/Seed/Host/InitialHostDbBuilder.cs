using DinePlan.DineConnect.EntityFramework;

namespace DinePlan.DineConnect.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly DineConnectDbContext _context;

        public InitialHostDbBuilder(DineConnectDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
