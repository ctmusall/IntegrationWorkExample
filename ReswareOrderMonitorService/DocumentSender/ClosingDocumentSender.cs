using ReswareOrderMonitorService.ReswareNoteDocs;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.DocumentSender
{
    internal abstract class ClosingDocumentSender : DocumentSender
    {
        internal override bool SendDocs(DocumentServiceResult document, OrderResult order)
        {
            return SendDocumentsToDocumentTeam(document, order) && SendActionEventToResware();
        }

        internal abstract bool SendDocumentsToDocumentTeam(DocumentServiceResult document, OrderResult order);

        internal abstract bool SendActionEventToResware();
    }
}
