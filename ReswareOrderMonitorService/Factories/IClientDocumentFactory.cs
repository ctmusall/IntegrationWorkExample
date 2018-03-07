namespace ReswareOrderMonitorService.Factories
{
    internal interface IClientDocumentFactory
    {
        IDocumentReaderFactory ResolveDocumentReaderFactory(int clientId);
    }
}
