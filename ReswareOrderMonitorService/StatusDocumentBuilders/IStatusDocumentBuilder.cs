using Aspose.Words;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusDocumentBuilders
{
    internal interface IStatusDocumentBuilder
    {
        Document BuildDocument(OrderResult reswareOrder, GetOrderResult eClosingOrder);
    }
}
