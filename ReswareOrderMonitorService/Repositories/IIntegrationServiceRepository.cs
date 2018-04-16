using ReswareOrderMonitorService.Models;

namespace ReswareOrderMonitorService.Repositories
{
    internal interface IIntegrationServiceRepository
    {
        EClosingOrder GetOrder(string customerId, string fileNumber);
    }
}
