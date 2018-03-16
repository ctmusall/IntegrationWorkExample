using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using ReswareOrderMonitorService.ReswareNoteDocs;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.Utilities.Solidifi
{
    internal class SolidifiClosingDocumentMailUtility : ClosingDocumentMailUtility
    {
        public override MailMessage BuildClosingDocumentMailMessage(DocumentServiceResult document, OrderResult reswareOrder)
        {
            var propertyAddress = reswareOrder.PropertyAddress.FirstOrDefault(o => o.OrderId == reswareOrder.Id);
            if (propertyAddress == null) return null;

            var body = new StringBuilder();
            body.AppendLine("File Number:");
            body.AppendLine(reswareOrder.FileNumber);
            body.AppendLine("Property Address:");
            body.AppendLine(propertyAddress.AddressStreetInfo);
            body.AppendLine($"{propertyAddress.City}, {propertyAddress.State} {propertyAddress.Zip}");
            body.AppendLine($"{propertyAddress.County} County");

            var mailMessage = new MailMessage
            {
                From = new MailAddress("solidifiresware@pcnclosings.com", "Solidifi Resware"),
                To = { "docs@pcnclosings.com" },
                Subject = $"Incoming Solidifi Closing Package for File Number {reswareOrder.FileNumber}",
                Body = body.ToString()
            };

            mailMessage.Attachments.Add(new Attachment(new MemoryStream(document.DocumentBody), document.FileName));

            return mailMessage;
        }

    }
}
