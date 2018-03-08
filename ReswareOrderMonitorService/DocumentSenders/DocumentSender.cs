using ReswareOrderMonitorService.ReswareNoteDocs;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.DocumentSenders
{
    internal abstract class DocumentSender
    {
        internal abstract bool SendDocs(DocumentServiceResult document, OrderResult order);
    }
}
