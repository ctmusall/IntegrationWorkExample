using Aspose.Words;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.Utilities
{
    internal interface IStatusDocumentUtility
    {
        Document BuildDocument(OrderResult reswareOrder, GetOrderResult eClosingOrder);
    }
}
