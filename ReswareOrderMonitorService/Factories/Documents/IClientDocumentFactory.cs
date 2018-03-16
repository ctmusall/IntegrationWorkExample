namespace ReswareOrderMonitorService.Factories.Documents
{
    internal interface IClientDocumentFactory
    {
        IDocumentReaderFactory ResolveDocumentReaderFactory(int clientId);
    }
}
