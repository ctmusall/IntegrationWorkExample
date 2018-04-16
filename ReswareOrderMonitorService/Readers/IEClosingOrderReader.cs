using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Models;

namespace ReswareOrderMonitorService.Readers
{
    internal interface IEClosingOrderReader
    {
        EClosingOrder MapEClosingOrder(GetOrderResult getOrderResult);
    }
}
