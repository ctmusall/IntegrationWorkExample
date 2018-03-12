using Aspose.Words;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.Utilities
{
    internal abstract class StatusDocumentUtility : IStatusDocumentUtility
    {
        public abstract Document BuildDocument(OrderResult reswareOrder, GetOrderResult eClosingOrder);
    }
}
