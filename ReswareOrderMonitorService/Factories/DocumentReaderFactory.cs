namespace ReswareOrderMonitorService.Factories
{
    internal abstract class DocumentReaderFactory : IDocumentReaderFactory
    {
        public abstract DocumentSender.DocumentSender ResolveDocumentSender(int documentTypeId);
    }
}
