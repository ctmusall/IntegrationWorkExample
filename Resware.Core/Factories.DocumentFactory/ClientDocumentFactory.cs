using Resware.Core.Factories.DocumentReaders;

namespace Resware.Core.Factories.DocumentFactory
{
    public class ClientDocumentFactory : IClientDocumentFactory
    {
        public DocumentReaderFactory ResolveDocumentReaderFactory(int clientId)
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
