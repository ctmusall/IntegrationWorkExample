using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.eClosingIntegrationService;

namespace ReswareOrderMonitorService.Factories.StatusSendersOrders
{
    internal class StatusSenderOrderFactory : IStatusSenderOrderFactory
    {
        private readonly IntegrationServiceClient _integrationServiceClient;

        internal StatusSenderOrderFactory()
        {
            _integrationServiceClient = new IntegrationServiceClient();
        }

        public GetOrderResult ResolveOrderResult(ServiceUtilityTypeEnum serviceUtilityType, string customerId, string fileNumber)
        {
            switch (serviceUtilityType)
            {
                case ServiceUtilityTypeEnum.Closing:
                    return _integrationServiceClient.GetOrder(customerId, fileNumber);
                case ServiceUtilityTypeEnum.TitleOpinion:
                    return _integrationServiceClient.GetOrder(customerId, $"{fileNumber}-T");
                case ServiceUtilityTypeEnum.DocPrep:
                    return _integrationServiceClient.GetOrder(customerId, $"{fileNumber}-D");
                default:
                    return null;
            }
        }
    }
}
