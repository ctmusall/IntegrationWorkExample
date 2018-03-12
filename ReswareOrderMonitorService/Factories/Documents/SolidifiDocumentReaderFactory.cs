using System.Collections.Generic;
using ReswareOrderMonitorService.DocumentSenders;
using ReswareOrderMonitorService.DocumentSenders.Solidifi;
using ReswareOrderMonitorService.Utilities.Solidifi;

namespace ReswareOrderMonitorService.Factories.Documents
{
    internal class SolidifiDocumentReaderFactory : DocumentReaderFactory
    {
        private readonly ICollection<int> _closingDocumentTypeIds = new List<int> { 1618, 1139, 1619, 1632 };

        public override DocumentSender ResolveDocumentSender(int documentTypeId)
        {
            return _closingDocumentTypeIds.Contains(documentTypeId) ? new SolidifiClosingDocumentSender(new SolidifiClosingDocumentMailUtility()) : null;
        }
    }
}
