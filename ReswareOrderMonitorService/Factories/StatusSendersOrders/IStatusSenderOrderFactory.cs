using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.eClosingIntegrationService;

namespace ReswareOrderMonitorService.Factories.StatusSendersOrders
{
    internal interface IStatusSenderOrderFactory
    {
        GetOrderResult ResolveOrderResult(ServiceUtilityTypeEnum serviceUtilityType, string clientId, string fileNumber);
    }
}
