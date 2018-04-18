using Resware.Core.Factories.DocumentReaders;

namespace Resware.Core.Factories.DocumentFactory
{
    public interface IClientDocumentFactory
    {
        DocumentReaderFactory ResolveDocumentReaderFactory(int clientId);
    }
}
