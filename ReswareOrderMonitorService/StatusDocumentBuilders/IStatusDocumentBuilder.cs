using Aspose.Words;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.eClosingIntegrationService;

namespace ReswareOrderMonitorService.StatusDocumentBuilders
{
    internal interface IStatusDocumentBuilder
    {
        Document BuildDocument(Order reswareOrder, GetOrderResult eClosingOrder);
    }
}
