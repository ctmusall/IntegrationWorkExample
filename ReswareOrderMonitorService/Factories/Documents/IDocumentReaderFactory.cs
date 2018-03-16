using ReswareOrderMonitorService.DocumentSenders;

namespace ReswareOrderMonitorService.Factories.Documents
{
    internal interface IDocumentReaderFactory
    {
        DocumentSender ResolveDocumentSender(int documentTypeId);
    }
}
