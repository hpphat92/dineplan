using Abp.Application.Services;
using DinePlan.DineConnect.Dto;
using DinePlan.DineConnect.Logging.Dto;

namespace DinePlan.DineConnect.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
