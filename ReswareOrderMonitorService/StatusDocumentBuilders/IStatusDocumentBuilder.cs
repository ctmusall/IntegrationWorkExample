using Aspose.Words;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.Models;

namespace ReswareOrderMonitorService.StatusDocumentBuilders
{
    internal interface IStatusDocumentBuilder
    {
        Document BuildDocument(Order reswareOrder, EClosingOrder eClosingOrder);
    }
}
