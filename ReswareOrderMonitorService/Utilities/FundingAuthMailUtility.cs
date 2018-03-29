using System.Net.Mail;
using Resware.Entities.Orders;

namespace ReswareOrderMonitorService.Utilities
{
    internal abstract class FundingAuthMailUtility : IFundingAuthMailUtility
    {
        public abstract MailMessage BuildFundingAuthMailMessage(Order reswareOrder);
    }
}
