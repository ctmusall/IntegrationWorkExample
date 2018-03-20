using ReswareOrderMonitorService.DocumentSenders;
using ReswareOrderMonitorService.Utilities.Solidifi;

namespace ReswareOrderMonitorService.Factories.Documents
{
    internal class SolidifiDocumentReaderFactory : DocumentReaderFactory
    {
        private const int ClosingDocumentTypeId = 1022;
        // TODO - Send document to disbursements (1618, 1139, 1619, 1623, 1632)

        public override DocumentSender ResolveDocumentSender(int documentTypeId)
        {
            return ClosingDocumentTypeId.Equals(documentTypeId) ? new ClosingDocumentSender(new SolidifiClosingDocumentMailUtility()) : null;
        }
    }
}
