using System.Collections.Generic;
using DinePlan.DineConnect.House.Master.Dtos;
using DinePlan.DineConnect.DataExporting.Excel.EpPlus;
using DinePlan.DineConnect.Dto;

namespace DinePlan.DineConnect.House.Master.Exporter
{
    public class SupplierListExcelExporter : EpPlusExcelExporterBase, ISupplierListExcelExporter
    {
        public FileDto ExportToFile(List<SupplierListDto> dtos)
        {
            return CreateExcelPackage(
                "SupplierList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Supplier"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("Address1"),
                        L("City")
                        );

                    AddObjects(
                        sheet, 2, dtos,
                        _ => _.SupplierName,
                        _ => _.Address1,
                        _ => _.City
                        );

                    for (var i = 1; i <= 1; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}