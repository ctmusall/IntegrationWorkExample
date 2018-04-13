using System.Net.Mail;
using Resware.Entities.Orders;

namespace ReswareOrderMonitorService.Utilities
{
    internal interface IFundingAuthMailUtility
    {
        MailMessage BuildFundingAuthMailMessage(Order reswareOrder);
        bool SendFundingAuthMailMessage(MailMessage mailMessage);
    }
}
