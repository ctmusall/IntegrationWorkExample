using ReswareOrderMonitorService.DocumentSenders;

namespace ReswareOrderMonitorService.Factories.Documents
{
    internal abstract class DocumentReaderFactory : IDocumentReaderFactory
    {
        public abstract DocumentSender ResolveDocumentSender(int documentTypeId);
    }
}
