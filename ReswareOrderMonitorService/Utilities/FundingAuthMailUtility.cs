using System.Net.Mail;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.Utilities
{
    internal abstract class FundingAuthMailUtility : IFundingAuthMailUtility
    {
        public abstract MailMessage BuildFundingAuthMailMessage(OrderResult reswareOrder);
    }
}
