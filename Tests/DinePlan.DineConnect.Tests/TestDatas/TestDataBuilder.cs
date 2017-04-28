using EntityFramework.DynamicFilters;
using DinePlan.DineConnect.EntityFramework;

namespace DinePlan.DineConnect.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly DineConnectDbContext _context;
        private readonly int _tenantId;

        public TestDataBuilder(DineConnectDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();

            _context.SaveChanges();
        }
    }
}
