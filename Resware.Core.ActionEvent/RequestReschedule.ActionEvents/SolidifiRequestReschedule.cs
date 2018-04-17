using eClosings.Data.IntegrationService.Repository;
using eClosings.Mirth.Clients;
using Resware.Core.ActionEvent.RequestClosing.ActionEvents;
using Resware.Core.Services.Utilities.ServiceUtilities;
using Resware.Data.Signing.Repository;
using Resware.Entities.Orders;

namespace Resware.Core.ActionEvent.RequestReschedule.ActionEvents
{
    internal class SolidifiRequestReschedule : ActionEvent.ActionEvents.ActionEvent
    {
        private readonly IIntegrationServiceRepository _integrationServiceRepository;
        private readonly ServiceUtility _orderServiceUtility;
        private readonly SigningRepository _receiveSigningServiceRepository;
        private readonly IMirthServiceClient _mirthServiceClient;

        internal SolidifiRequestReschedule(ServiceUtility orderServiceUtility, IIntegrationServiceRepository integrationServiceRepository, SigningRepository receiveSigningServiceRepository, IMirthServiceClient mirthServiceClient)
        {
            _orderServiceUtility = orderServiceUtility;
            _integrationServiceRepository = integrationServiceRepository;
            _receiveSigningServiceRepository = receiveSigningServiceRepository;
            _mirthServiceClient = mirthServiceClient;
        }

        internal override bool PerformAction(Order order)
        {
            var existingOrder = _integrationServiceRepository.GetOrder(order.CustomerId, order.FileNumber);
            if (existingOrder == null)
            {
                order.Notes += $"Received Reschedule Action Event from Resware for file number {order.FileNumber}. Order did not exist in eClosings.";
            }
            return new SolidifiRequestClosing(_receiveSigningServiceRepository, _mirthServiceClient, _orderServiceUtility).PerformAction(order);
        }
    }
}
