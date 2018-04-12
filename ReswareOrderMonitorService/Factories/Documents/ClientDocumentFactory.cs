namespace ReswareOrderMonitorService.Factories.Documents
{
    internal class ClientDocumentFactory : IClientDocumentFactory
    {
        public IDocumentReaderFactory ResolveDocumentReaderFactory(int clientId)
        {
            switch (clientId)
            {
                case 1:
                    return new SolidifiDocumentReaderFactory();
                default:
                    return null;
            }
        }
    }
}
