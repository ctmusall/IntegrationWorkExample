using System.Collections.Generic;
using ReswareOrderMonitorService.DocumentSenders;
using ReswareOrderMonitorService.Utilities.Solidifi;

namespace ReswareOrderMonitorService.Factories.Documents
{
    internal class SolidifiDocumentReaderFactory : DocumentReaderFactory
    {
        private const int ClosingDocumentTypeId = 1022;
        private readonly ICollection<int> _disbursementDocumentTypeIds = new List<int> { 1618, 1139, 1619, 1623, 1632 }; 

        public override DocumentSender ResolveDocumentSender(int documentTypeId)
        {
            if (_disbursementDocumentTypeIds.Contains(documentTypeId)) return new DocumentSender(new SolidifiDisbursementDocumentMailUtility());

            return ClosingDocumentTypeId.Equals(documentTypeId) ? new DocumentSender(new SolidifiClosingDocumentMailUtility()) : null;
        }
    }
}
