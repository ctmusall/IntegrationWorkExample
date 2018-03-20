using System.Net.Mail;
using ReswareOrderMonitorService.ReswareNoteDocs;
using ReswareOrderMonitorService.ReswareOrders;
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

        internal bool SendDocs(DocumentServiceResult document, OrderResult order)
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
