using System.Net.Mail;
using ReswareOrderMonitorService.ReswareNoteDocs;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.Utilities
{
    internal interface IClosingDocumentMailUtility
    {
        MailMessage BuildClosingDocumentMailMessage(DocumentServiceResult document, OrderResult reswareOrder);
    }
}
