using ReswareOrderMonitorService.ActionEvents.Solidifi;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Mirth;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.ActionEvents
{
    internal class SchedulingReschedule : ActionEvent
    {
        private readonly IIntegrationServiceRepository _integrationServiceRepository;
        private readonly IServiceUtility _orderServiceUtility;
        private readonly IReceiveSigningServiceRepository _receiveSigningServiceRepository;
        private readonly IMirthServiceClient _mirthServiceClient;

        internal SchedulingReschedule(IServiceUtility orderServiceUtility, IIntegrationServiceRepository integrationServiceRepository, IReceiveSigningServiceRepository receiveSigningServiceRepository, IMirthServiceClient mirthServiceClient)
        {
            _orderServiceUtility = orderServiceUtility;
            _integrationServiceRepository = integrationServiceRepository;
            _receiveSigningServiceRepository = receiveSigningServiceRepository;
            _mirthServiceClient = mirthServiceClient;
        }

        internal override bool PerformAction(OrderResult order)
        {
            var existingOrder = _integrationServiceRepository.GetOrder(order.CustomerId, order.FileNumber);
            if (existingOrder.Outcome == OutcomeEnum.Fail || existingOrder.Order == null)
            {
                order.Notes += $"Received Reschedule Action Event from Resware for file number {order.FileNumber}. Order did not exist in eClosings.";
            }
            return ReturnNewClosing(_receiveSigningServiceRepository, _mirthServiceClient, _orderServiceUtility).PerformAction(order);
        }

        protected internal RequestOrder ReturnNewClosing(IReceiveSigningServiceRepository receiveSigningServiceRepository, IMirthServiceClient mirthServiceClient,IServiceUtility serviceUtility)
        {
            return new SolidifiRequestClosing(receiveSigningServiceRepository, mirthServiceClient, serviceUtility);
        }
    }
}
