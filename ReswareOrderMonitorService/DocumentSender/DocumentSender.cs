using ReswareOrderMonitorService.ReswareNoteDocs;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.DocumentSender
{
    internal abstract class DocumentSender
    {
        internal abstract bool SendDocs(DocumentServiceResult document, OrderResult order);
    }
}
