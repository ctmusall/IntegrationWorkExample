namespace ReswareOrderMonitorService.Factories.Documents
{
    internal class ClientDocumentFactory : IClientDocumentFactory
    {
        public IDocumentReaderFactory ResolveDocumentReaderFactory(int clientId)
        {
            switch (clientId)
            {
                // TODO - Switch on Solidifi client id
                default:
                    return new SolidifiDocumentReaderFactory(); 
            }
        }
    }
}
