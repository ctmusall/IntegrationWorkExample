using System.Net.Mail;
using Resware.Entities.Notes.Documents;
using Resware.Entities.Orders;

namespace Resware.Core.Utilities.DocumentMail
{
    public interface IDocumentMailUtility
    {
        bool SendDocumentMailMessage(MailMessage mailMessage);
        MailMessage BuildDocumentMailMessage(Document document, Order reswareOrder);
    }
}
