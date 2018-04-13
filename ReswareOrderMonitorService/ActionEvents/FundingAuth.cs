using Resware.Entities.Orders;
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

        internal override bool PerformAction(Order order)
        {
            var mailMessage = _fundingAuthMailUtility.BuildFundingAuthMailMessage(order);

            return mailMessage != null && _fundingAuthMailUtility.SendFundingAuthMailMessage(mailMessage);
        }
    }
}
