using System.Net.Mail;
using Resware.Entities.Notes.Documents;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.DocumentSenders
{
    internal class DocumentSender
    {
        private readonly IDocumentMailUtility _documentMailUtility;

        internal DocumentSender(IDocumentMailUtility documentMailUtility)
        {
            _documentMailUtility = documentMailUtility;
        }

        internal bool SendDocs(Document document, Order order)
        {
            var mailMessage = _documentMailUtility.BuildDocumentMailMessage(document, order);
            if (mailMessage == null) return false;
            var smtpSender = new SmtpClient("outlook.pcnclosings.com", 25);
            smtpSender.Send(mailMessage);
            // TODO - Send to resware utility -- send action event closing package received
            return true;
        }
    }
}
