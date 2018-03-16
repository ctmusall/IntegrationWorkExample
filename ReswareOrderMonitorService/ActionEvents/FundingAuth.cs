using System.Net.Mail;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.ActionEvents
{
    internal class FundingAuth : ActionEvent
    {
        private readonly IFundingAuthMailUtility _fundingAuthMailUtility;

        internal FundingAuth(IFundingAuthMailUtility fundingAuthMailUtility)
        {
            _fundingAuthMailUtility = fundingAuthMailUtility;
        }

        internal override bool PerformAction(OrderResult order)
        {
            var mailMessage = _fundingAuthMailUtility.BuildFundingAuthMailMessage(order);

            if (mailMessage == null) return false;

            var smtpSender = new SmtpClient("outlook.pcnclosings.com", 25);
            smtpSender.Send(mailMessage);

            return true;
        }
    }
}
