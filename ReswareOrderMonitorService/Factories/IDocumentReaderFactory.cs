namespace ReswareOrderMonitorService.Factories
{
    internal interface IDocumentReaderFactory
    {
        DocumentSender.DocumentSender ResolveDocumentSender(int documentTypeId);
    }
}
