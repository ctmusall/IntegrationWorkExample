using Resware.Core.ActionEvent.FundingAuth.MailUtility;
using Resware.Entities.Orders;

namespace Resware.Core.ActionEvent.RequestFundingAuth.ActionEvents
{
    internal class RequestFundingAuth : ActionEvent.ActionEvents.ActionEvent
    {
        private readonly FundingAuthMailUtility _fundingAuthMailUtility;

        internal RequestFundingAuth(FundingAuthMailUtility fundingAuthMailUtility)
        {
            _fundingAuthMailUtility = fundingAuthMailUtility;
        }

        internal override bool PerformAction(Order order)
        {
            var mailMessage = _fundingAuthMailUtility.BuildFundingAuthMailMessage(order);

            return mailMessage != null && _fundingAuthMailUtility.SendFundingAuthMailMessage(mailMessage);
        }
    }
}
