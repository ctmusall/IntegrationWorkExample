using System.Net.Mail;
using Resware.Entities.Notes.Documents;
using Resware.Entities.Orders;

namespace ReswareOrderMonitorService.Utilities
{
    internal interface IDocumentMailUtility
    {
        MailMessage BuildDocumentMailMessage(Document document, Order reswareOrder);
    }
}
