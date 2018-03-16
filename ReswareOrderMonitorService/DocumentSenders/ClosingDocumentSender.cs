using System.Net.Mail;
using ReswareOrderMonitorService.ReswareNoteDocs;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.DocumentSenders
{
    internal class ClosingDocumentSender : DocumentSender
    {
        private readonly IClosingDocumentMailUtility _closingDocumentMailUtility;

        internal ClosingDocumentSender(IClosingDocumentMailUtility closingDocumentMailUtility)
        {
            _closingDocumentMailUtility = closingDocumentMailUtility;
        }

        internal override bool SendDocs(DocumentServiceResult document, OrderResult order)
        {
            var mailMessage = _closingDocumentMailUtility.BuildClosingDocumentMailMessage(document, order);
            var smtpSender = new SmtpClient("outlook.pcnclosings.com", 25);
            smtpSender.Send(mailMessage);
            return true;
            // TODO - Send to resware utility -- send action event closing package received
        }
    }
}
