using System.Linq;
using System.Net.Mail;
using System.Text;
using Resware.Entities.Orders;
using ReswareCommon.Enums;

namespace ReswareOrderMonitorService.Utilities.Solidifi
{
    internal class SolidifiFundingAuthMailUtility : FundingAuthMailUtility
    {
        public override MailMessage BuildFundingAuthMailMessage(Order reswareOrder)
        {
            var propertyAddress = reswareOrder.PropertyAddress.FirstOrDefault(o => o.OrderId == reswareOrder.Id);
            if (propertyAddress == null) return null;

            var borrower = reswareOrder.BuyerAndSellers.FirstOrDefault(b => b.Type == BuyerSellerEnum.Buyer && !b.Spouse);
            if (borrower == null) return null;

            var coBorrower = reswareOrder.BuyerAndSellers.FirstOrDefault(b => b.Type == BuyerSellerEnum.Buyer && b.Spouse);

            var mailMessage = new MailMessage
            {
                From = new MailAddress("solidifiresware@pcnclosings.com", "Solidifi Resware"),
                To = { "disbursements@pcnclosings.com" },
                Subject = $"{propertyAddress.AddressStreetInfo}, {propertyAddress.City}, {propertyAddress.State}, {propertyAddress.Zip}: {borrower.FirstName} {borrower.MiddleName} {borrower.LastName} {coBorrower?.FirstName} {coBorrower?.MiddleName} {coBorrower?.LastName}: Authorization to Disburse"
            };

            var body = new StringBuilder();
            body.AppendLine("You are authorized to fund this file. Please contact us immediately if anything is missing (funds/documents) that will be needed to disburse.");
            body.AppendLine($"Property: {propertyAddress.AddressStreetInfo}, {propertyAddress.City}, {propertyAddress.State} {propertyAddress.Zip}");
            body.AppendLine($"Borrowers: {borrower.FirstName} {borrower.MiddleName} {borrower.LastName} {coBorrower?.FirstName} {coBorrower?.MiddleName} {coBorrower?.LastName}");
            body.AppendLine($"Loan Number: {reswareOrder.FileNumber}");
            body.AppendLine("Thank you");
            mailMessage.Body = body.ToString();

            return mailMessage;
        }

        public override bool SendFundingAuthMailMessage(MailMessage mailMessage)
        {
            var smtpSender = new SmtpClient("outlook.pcnclosings.com", 25);
            smtpSender.Send(mailMessage);

            return true;
        }
    }
}
