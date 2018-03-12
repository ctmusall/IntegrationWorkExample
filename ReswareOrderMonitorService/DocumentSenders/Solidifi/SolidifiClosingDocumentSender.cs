using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.DocumentSenders.Solidifi
{
    internal class SolidifiClosingDocumentSender : ClosingDocumentSender
    {
        internal SolidifiClosingDocumentSender(IClosingDocumentMailUtility closingDocumentMailUtility) : base(closingDocumentMailUtility) { }
    }
}
