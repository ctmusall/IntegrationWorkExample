using Resware.Core.DocumentSenders;

namespace Resware.Core.Factories.DocumentReaders
{
    public abstract class DocumentReaderFactory
    {
        public abstract DocumentSender ResolveDocumentSender(int documentTypeId);
    }
}
