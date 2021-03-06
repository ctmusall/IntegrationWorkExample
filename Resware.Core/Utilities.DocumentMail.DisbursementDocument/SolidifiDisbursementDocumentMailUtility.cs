﻿using System.Net.Mail;
using Resware.Core.Utilities.DocumentMail;

namespace Resware.Core.DocumentMailUtilities.DisbursementDocumentMailUtility
{
    internal class SolidifiDisbursementDocumentMailUtility : DocumentMailUtility
    {
        protected internal override MailMessage CreateMailMessage(string fileNumber)
        {
            return new MailMessage
            {
                From = new MailAddress("solidifiresware@pcnclosings.com", "Solidifi Resware"),
                To = { "disbursements@pcnclosings.com" },
                Subject = $"Incoming Disbursement Documents for File Number {fileNumber}"
            };
        }
    }
}
