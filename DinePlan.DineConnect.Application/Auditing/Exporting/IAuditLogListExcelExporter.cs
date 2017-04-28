using System.Collections.Generic;
using DinePlan.DineConnect.Auditing.Dto;
using DinePlan.DineConnect.Dto;

namespace DinePlan.DineConnect.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);
    }
}
