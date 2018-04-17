using System.Net.Mail;
using Resware.Core.Utilities.DocumentMail;

namespace Resware.Core.DocumentMailUtilities.ClosingDocumentMailUtility
{
    internal class SolidifiClosingDocumentMailUtility : DocumentMailUtility
    {
        protected internal override MailMessage CreateMailMessage(string fileNumber)
        {
            return new MailMessage
            {
                From = new MailAddress("solidifiresware@pcnclosings.com", "Solidifi Resware"),
                To = { "docs@pcnclosings.com" },
                Subject = $"Incoming Solidifi Closing Package for File Number {fileNumber}"
            };
        }
    }
}
