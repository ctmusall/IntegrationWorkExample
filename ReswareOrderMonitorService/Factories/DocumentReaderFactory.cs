using ReswareOrderMonitorService.DocumentSenders;

namespace ReswareOrderMonitorService.Factories
{
    internal abstract class DocumentReaderFactory : IDocumentReaderFactory
    {
        public abstract DocumentSender ResolveDocumentSender(int documentTypeId);
    }
}
