using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using ReswareOrderMonitorService.ReswareNoteDocs;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.DocumentSenders.Solidifi
{
    internal class SolidifiClosingDocumentSender : ClosingDocumentSender
    {
        internal override bool SendDocumentsToDocumentTeam(DocumentServiceResult document, OrderResult order)
        {
            try
            {
                var mailMessage = BuildClosingDocumentMailMessage(document, order);
                var smtpSender = new SmtpClient("outlook.pcnclosings.com", 25);
                smtpSender.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        internal override bool SendActionEventToResware()
        {
            return true;
        }

        private static MailMessage BuildClosingDocumentMailMessage(DocumentServiceResult document, OrderResult order)
        {
            var propertyAddress = order.PropertyAddress.FirstOrDefault(o => o.OrderId == order.Id);
            if (propertyAddress == null) return null;

            var body = new StringBuilder();
            body.AppendLine("File Number:");
            body.AppendLine(order.FileNumber);
            body.AppendLine("Property Address:");
            body.AppendLine(propertyAddress.AddressStreetInfo);
            body.AppendLine($"{propertyAddress.City}, {propertyAddress.State} {propertyAddress.Zip}");
            body.AppendLine($"{propertyAddress.County} County");

            var mailMessage = new MailMessage
            {
                From = new MailAddress("solidifiresware@pcnclosings.com", "Solidifi Resware"),
                To = { "docs@pcnclosings.com" },
                Subject = $"Incoming Solidifi Closing Package for File Number {order.FileNumber}",
                Body = body.ToString()
            };

            mailMessage.Attachments.Add(new Attachment(new MemoryStream(document.DocumentBody), document.FileName));

            return mailMessage;
        }
    }
}
