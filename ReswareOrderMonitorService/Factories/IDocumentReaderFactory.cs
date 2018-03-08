using ReswareOrderMonitorService.DocumentSenders;

namespace ReswareOrderMonitorService.Factories
{
    internal interface IDocumentReaderFactory
    {
        DocumentSender ResolveDocumentSender(int documentTypeId);
    }
}
