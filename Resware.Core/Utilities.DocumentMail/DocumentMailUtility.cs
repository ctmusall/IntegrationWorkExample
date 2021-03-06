﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Resware.Entities.Notes.Documents;
using Resware.Entities.Orders;

namespace Resware.Core.Utilities.DocumentMail
{
    internal abstract class DocumentMailUtility : IDocumentMailUtility
    {
        public MailMessage BuildDocumentMailMessage(Document document, Order reswareOrder)
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

            var mailMessage = CreateMailMessage(reswareOrder.FileNumber);

            mailMessage.Body = body.ToString();

            mailMessage.Attachments.Add(new Attachment(new MemoryStream(document.DocumentBody), document.FileName));

            return mailMessage;
        }

        protected internal abstract MailMessage CreateMailMessage(string fileNumber);

        public bool SendDocumentMailMessage(MailMessage mailMessage)
        {
            try
            {
                var smtpSender = new SmtpClient("outlook.pcnclosings.com", 25);
                smtpSender.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message, EventLogEntryType.Error);
                return false;
            }
        }
    }
}
