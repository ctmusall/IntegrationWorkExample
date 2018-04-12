using Resware.Data.Signing.Repository;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Mirth;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.ActionEvents.Solidifi
{
    internal class SolidifiSchedulingReschedule : ActionEvent
    {
        private readonly IIntegrationServiceRepository _integrationServiceRepository;
        private readonly IServiceUtility _orderServiceUtility;
        private readonly SigningRepository _receiveSigningServiceRepository;
        private readonly IMirthServiceClient _mirthServiceClient;

        internal SolidifiSchedulingReschedule(IServiceUtility orderServiceUtility, IIntegrationServiceRepository integrationServiceRepository, SigningRepository receiveSigningServiceRepository, IMirthServiceClient mirthServiceClient)
        {
            _orderServiceUtility = orderServiceUtility;
            _integrationServiceRepository = integrationServiceRepository;
            _receiveSigningServiceRepository = receiveSigningServiceRepository;
            _mirthServiceClient = mirthServiceClient;
        }

        internal override bool PerformAction(Order order)
        {
            var existingOrder = _integrationServiceRepository.GetOrder(order.CustomerId, order.FileNumber);
            if (existingOrder.Outcome == OutcomeEnum.Fail || existingOrder.Order == null)
            {
                order.Notes += $"Received Reschedule Action Event from Resware for file number {order.FileNumber}. Order did not exist in eClosings.";
            }
            return new SolidifiRequestClosing(_receiveSigningServiceRepository, _mirthServiceClient, _orderServiceUtility).PerformAction(order);
        }
    }
}
