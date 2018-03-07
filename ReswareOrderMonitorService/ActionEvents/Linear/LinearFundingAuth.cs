using System;
using System.Linq;
using System.Net.Mail;
using System.Text;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.ActionEvents.Linear
{
    internal class LinearFundingAuth : FundingAuth
    {
        internal override MailMessage BuildFundingAuthMailMessage(OrderResult order)
        {
            var propertyAddress = order.PropertyAddress.FirstOrDefault(o => o.OrderId == order.Id);
            if (propertyAddress == null) return null;

            var borrower = order.BuyersAndSellers.FirstOrDefault(b => b.Type == BuyerSellerEnum.Buyer && !b.Spouse);
            if (borrower == null) return null;

            var coBorrower = order.BuyersAndSellers.FirstOrDefault(b => b.Type == BuyerSellerEnum.Buyer && b.Spouse);

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
            body.AppendLine($"Loan Number: {order.FileNumber}");
            body.AppendLine("Thank you");
            mailMessage.Body = body.ToString();

            return mailMessage;
        }

        internal override bool SendFundingAuthMailMessage(MailMessage mailMessage)
        {
            try
            {
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
    }
}
