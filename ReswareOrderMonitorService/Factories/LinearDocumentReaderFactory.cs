using System.Collections.Generic;
using ReswareOrderMonitorService.DocumentSender;

namespace ReswareOrderMonitorService.Factories
{
    internal class LinearDocumentReaderFactory : DocumentReaderFactory
    {
        private readonly ICollection<int> _closingDocumentTypeIds = new List<int> { 1618, 1139, 1619, 1632 };

        public override DocumentSender.DocumentSender ResolveDocumentSender(int documentTypeId)
        {
            return _closingDocumentTypeIds.Contains(documentTypeId) ? new LinearClosingDocumentSender() : null;
        }
    }
}
