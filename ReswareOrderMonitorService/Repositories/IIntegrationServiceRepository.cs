using ReswareOrderMonitorService.eClosingIntegrationService;

namespace ReswareOrderMonitorService.Repositories
{
    internal interface IIntegrationServiceRepository
    {
        GetOrderResult GetOrder(string customerId, string fileNumber);
    }
}
