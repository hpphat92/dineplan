
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using DinePlan.DineConnect.House.Master.Dtos;
using DinePlan.DineConnect.Dto;
using Abp.UI;
using DinePlan.DineConnect.House.Impl;
using System;

namespace DinePlan.DineConnect.House.Master.Implementation
{
    public class SupplierAppService : DineConnectAppServiceBase, ISupplierAppService
    {

        private readonly ISupplierListExcelExporter _supplierExporter;
        private readonly ISupplierManager _supplierManager;
        private readonly IRepository<Supplier> _supplierRepo;

        public SupplierAppService(ISupplierManager supplierManager,
            IRepository<Supplier> supplierRepo,
            ISupplierListExcelExporter supplierExporter)
        {
            _supplierManager = supplierManager;
            _supplierRepo = supplierRepo;
            _supplierExporter = supplierExporter;

        }

        public async Task<PagedResultOutput<SupplierListDto>> GetAll(GetSupplierInput input)
        {
            IQueryable<Supplier> allItems;
            if (!string.IsNullOrEmpty(input.Operation) && input.Operation.Equals("SEARCH"))
            {
                allItems = _supplierRepo
               .GetAll()
               .WhereIf(
                   !input.Filter.IsNullOrEmpty(),
                   p => p.SupplierName.Equals(input.Filter)
               );
            }
            else
            {
                allItems = _supplierRepo
               .GetAll()
               .WhereIf(
                   !input.Filter.IsNullOrEmpty(),
                   p => p.SupplierName.Contains(input.Filter)
               );
            }
            var sortMenuItems = await allItems
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var allListDtos = sortMenuItems.MapTo<List<SupplierListDto>>();

            var allItemCount = await allItems.CountAsync();

            return new PagedResultOutput<SupplierListDto>(
                allItemCount,
                allListDtos
                );
        }


        public IdInput GetSupplierIdByName(string suppliername)
        {
            var lstSupId = _supplierRepo.GetAllList(a => a.SupplierName.Equals(suppliername)).ToList().FirstOrDefault();
            return new IdInput
            {
                Id = lstSupId !=null ? lstSupId.Id : 0
            };
        }

        public async Task<FileDto> GetAllToExcel(GetSupplierInput input)
        {
            input.MaxResultCount = 10000;

            var allList = await GetAll(input);
            var allListDtos = allList.Items.MapTo<List<SupplierListDto>>();
            return _supplierExporter.ExportToFile(allListDtos);
        }

        public async Task<GetSupplierForEditOutput> GetSupplierForEdit(NullableIdInput input)
        {
            SupplierEditDto editDto;

            if (input.Id.HasValue)
            {
                var hDto = await _supplierRepo.GetAsync(input.Id.Value);
                editDto = hDto.MapTo<SupplierEditDto>();
            }
            else
            {
                editDto = new SupplierEditDto();
            }

            return new GetSupplierForEditOutput
            {
                Supplier = editDto
            };
        }

        public async Task CreateOrUpdateSupplier(CreateOrUpdateSupplierInput input)
        {
            if (input.Supplier.Id.HasValue)
            {
                await UpdateSupplier(input);
            }
            else
            {
                await CreateSupplier(input);
            }
        }

        public async Task DeleteSupplier(IdInput input)
        {
            await _supplierRepo.DeleteAsync(input.Id);
        }

        protected virtual async Task UpdateSupplier(CreateOrUpdateSupplierInput input)
        {
            var item = await _supplierRepo.GetAsync(input.Supplier.Id.Value);
            var dto = input.Supplier;

            item.SupplierName = dto.SupplierName;
            item.Address1 = dto.Address1;
            item.Address2 = dto.Address2;
            item.Address3 = dto.Address3;
            item.City = dto.City;
            item.State = dto.State;
            item.Country = dto.Country;
            item.ZipCode = dto.ZipCode;
            item.PhoneNumber1 = dto.PhoneNumber1;
            item.Email = dto.Email;
            item.FaxNumber = dto.FaxNumber;
            item.Website = dto.Website;
            item.DefaultCreditDays = dto.DefaultCreditDays;
            item.OrderPlacedThrough = dto.OrderPlacedThrough;
            item.TaxRegistrationNumber = dto.TaxRegistrationNumber;

            CheckErrors(await _supplierManager.CreateSync(item));

        }

        protected virtual async Task CreateSupplier(CreateOrUpdateSupplierInput input)
        {
            var dto = input.Supplier.MapTo<Supplier>();

            CheckErrors(await _supplierManager.CreateSync(dto));
        }

        public async Task<ListResultOutput<SupplierListDto>> GetSupplierNames()
        {
            var lstSupplier = await _supplierRepo.GetAll().ToListAsync();
            return new ListResultOutput<SupplierListDto>(lstSupplier.MapTo<List<SupplierListDto>>());
        }

        public async Task<ListResultOutput<ComboboxItemDto>> GetSupplierForCombobox()
        {
            var lstSupplier = await _supplierRepo.GetAll().ToListAsync();
            return
                new ListResultOutput<ComboboxItemDto>(
                    lstSupplier.Select(e => new ComboboxItemDto(e.Id.ToString(), e.SupplierName)).ToList());
        }

        public async Task<ListResultOutput<ComboboxItemDto>> GetDocumentTypeForCombobox(NullableIdInput ninput)
        {
            List<ComboboxItemDto> retList = new List<ComboboxItemDto>();

            string enumstring;
            Array EnumValues = System.Enum.GetValues(typeof(DocumentType));
            foreach (int EnumValue in EnumValues)
            {
                enumstring = Enum.GetName(typeof(DocumentType), EnumValue);
                retList.Add(new ComboboxItemDto { Value = EnumValue.ToString(), DisplayText = enumstring });
            }

            return
                   new ListResultOutput<ComboboxItemDto>(
                       retList.Select(e => new ComboboxItemDto(e.Value.ToString(), e.DisplayText)).ToList());
        }

    }
}
