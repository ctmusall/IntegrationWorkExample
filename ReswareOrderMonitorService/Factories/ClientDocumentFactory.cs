namespace ReswareOrderMonitorService.Factories
{
    internal class ClientDocumentFactory : IClientDocumentFactory
    {
        public IDocumentReaderFactory ResolveDocumentReaderFactory(int clientId)
        {
            switch (clientId)
            {
                // TODO - Switch on linear client id
                default:
                    return new LinearDocumentReaderFactory(); 
            }
        }
    }
}
