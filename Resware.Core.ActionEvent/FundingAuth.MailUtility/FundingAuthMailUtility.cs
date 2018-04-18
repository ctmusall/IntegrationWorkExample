using System.Net.Mail;
using Resware.Entities.Orders;

namespace Resware.Core.ActionEvent.FundingAuth.MailUtility
{
    public abstract class FundingAuthMailUtility
    {
        public abstract MailMessage BuildFundingAuthMailMessage(Order reswareOrder);
        public abstract bool SendFundingAuthMailMessage(MailMessage mailMessage);
    }
}
