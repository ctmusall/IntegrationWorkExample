using System.Net.Mail;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.Utilities
{
    internal interface IFundingAuthMailUtility
    {
        MailMessage BuildFundingAuthMailMessage(OrderResult reswareOrder);
    }
}
