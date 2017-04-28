using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DinePlan.DineConnect.House.Master.Dtos;
using DinePlan.DineConnect.Dto;

namespace DinePlan.DineConnect.House.Master
{
    public interface ISupplierAppService : IApplicationService
    {
        Task<PagedResultOutput<SupplierListDto>> GetAll(GetSupplierInput inputDto);
        Task<FileDto> GetAllToExcel(GetSupplierInput input);
        Task<GetSupplierForEditOutput> GetSupplierForEdit(NullableIdInput nullableIdInput);
        Task CreateOrUpdateSupplier(CreateOrUpdateSupplierInput input);
        Task DeleteSupplier(IdInput input);

        Task<ListResultOutput<SupplierListDto>> GetSupplierNames();
        IdInput GetSupplierIdByName(string suppliername);
        Task<ListResultOutput<ComboboxItemDto>> GetSupplierForCombobox();

        Task<ListResultOutput<ComboboxItemDto>> GetDocumentTypeForCombobox(NullableIdInput ninput);

    }
}