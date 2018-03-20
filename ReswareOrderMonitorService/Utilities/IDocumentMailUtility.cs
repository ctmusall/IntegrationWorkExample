using System.Net.Mail;
using ReswareOrderMonitorService.ReswareNoteDocs;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.Utilities
{
    internal interface IDocumentMailUtility
    {
        MailMessage BuildDocumentMailMessage(DocumentServiceResult document, OrderResult reswareOrder);
    }
}
