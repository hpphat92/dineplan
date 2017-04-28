using System.Collections.Generic;
using DinePlan.DineConnect.House.Master.Dtos;
using DinePlan.DineConnect.Dto;

namespace DinePlan.DineConnect.House.Master
{
    public interface ISupplierListExcelExporter
    {
        FileDto ExportToFile(List<SupplierListDto> dtos);
    }
}