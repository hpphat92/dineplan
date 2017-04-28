
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace DinePlan.DineConnect.House.Impl
{
    public interface ISupplierManager
    {
        Task<IdentityResult> CreateSync(Supplier supplier);
    }
}