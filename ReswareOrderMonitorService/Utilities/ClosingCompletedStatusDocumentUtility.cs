using System;
using Aspose.Words;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.Utilities
{
    internal class ClosingCompletedStatusDocumentUtility : StatusDocumentUtility
    {
        protected internal override void AddBody(DocumentBuilder documentBuilder, OrderResult reswareOrder, GetOrderResult eClosingOrder)
        {
            throw new NotImplementedException();
        }
    }
}
