using Resware.Core.Utilities.DocumentMail;
using Resware.Entities.Notes.Documents;
using Resware.Entities.Orders;

namespace Resware.Core.DocumentSenders
{
    public class DocumentSender
    {
        private readonly IDocumentMailUtility _documentMailUtility;

        internal DocumentSender(IDocumentMailUtility documentMailUtility)
        {
            _documentMailUtility = documentMailUtility;
        }

        public bool SendDocs(Document document, Order order)
        {
            var mailMessage = _documentMailUtility.BuildDocumentMailMessage(document, order);
            return mailMessage != null && _documentMailUtility.SendDocumentMailMessage(mailMessage);
            // TODO - Send to resware utility -- send action event closing package received
        }
    }
}
