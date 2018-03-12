using System.Net.Mail;
using ReswareOrderMonitorService.ReswareNoteDocs;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.Utilities
{
    internal abstract class ClosingDocumentMailUtility : IClosingDocumentMailUtility
    {
        public abstract MailMessage BuildClosingDocumentMailMessage(DocumentServiceResult document, OrderResult reswareOrder);
    }
}
