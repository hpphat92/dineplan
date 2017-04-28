
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Microsoft.AspNet.Identity;

namespace DinePlan.DineConnect.House.Impl
{
    public class SupplierManager : DineConnectServiceBase, ISupplierManager, ITransientDependency
    {

        private readonly IRepository<Supplier> _supplierRepo;

        public SupplierManager(IRepository<Supplier> supplier)
        {
            _supplierRepo = supplier;
        }

        public async Task<IdentityResult> CreateSync(Supplier supplier)
        {
            if (supplier.Id == 0)
            {
                if (_supplierRepo.GetAll().Any(a => a.SupplierName.Equals(supplier.SupplierName)))
                {
                    string[] strArrays = { L("NameAlreadyExists") };
                    var success = AbpIdentityResult.Failed(strArrays);
                    return success;
                }

            

                if (!string.IsNullOrEmpty(supplier.TaxRegistrationNumber))
                {
                    if (_supplierRepo.GetAll().Any(a => a.TaxRegistrationNumber.Equals(supplier.TaxRegistrationNumber)))
                    {
                        string[] strArrays = { L("TaxRegnNumberAlreadyExists") };
                        var success = AbpIdentityResult.Failed(strArrays);
                        return success;
                    }
                }

                await _supplierRepo.InsertAndGetIdAsync(supplier);
                return IdentityResult.Success;
            }
            else
            {
                List<Supplier> lst = _supplierRepo.GetAll().Where(a => a.SupplierName.Equals(supplier.SupplierName) && a.Id != supplier.Id).ToList();
                if (lst.Count > 0)
                {
                    string[] strArrays = { L("NameAlreadyExists") };
                    var success = AbpIdentityResult.Failed(strArrays);
                    return success;
                }

               
                if (!string.IsNullOrEmpty(supplier.TaxRegistrationNumber))
                {
                    if (_supplierRepo.GetAll().Any(a => a.TaxRegistrationNumber.Equals(supplier.TaxRegistrationNumber) && a.Id  != supplier.Id))
                    {
                        string[] strArrays = { L("TaxRegnNumberAlreadyExists") };
                        var success = AbpIdentityResult.Failed(strArrays);
                        return success;
                    }
                }
                return IdentityResult.Success;
            }
        }

    }
}
