using System.Net.Mail;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.ActionEvents
{
    internal abstract class FundingAuth : ActionEvent
    {
        internal override bool PerformAction(OrderResult order)
        {
            var mailMessage = BuildFundingAuthMailMessage(order);
            return SendFundingAuthMailMessage(mailMessage);
        }

        internal abstract MailMessage BuildFundingAuthMailMessage(OrderResult order);

        internal abstract bool SendFundingAuthMailMessage(MailMessage mailMessage);
    }
}
