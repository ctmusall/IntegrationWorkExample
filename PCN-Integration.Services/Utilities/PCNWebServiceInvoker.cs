using System;
using PCN_Integration.Services.Common;
using PCN_Integration.Services.PcnIntegrationServiceTest;

namespace PCN_Integration.Services.Utilities
{
    public class PcnWebServiceInvoker : IDisposable
    {
        private IIntegrationService _client;
        private GetOrderResponse _getOrderResponse;

        public PcnWebServiceInvoker()
        {
            _client = new IntegrationServiceClient();      
        }

        public bool OrderExists(string customerId, string orderId)
        {
            var result = GetOrderFromService(customerId, orderId);
            return string.Equals(result.GetOrderResult.Message, PcnIntegrationServicesConstants.OrderValidationMessages.OrderFound) && string.Equals(result.GetOrderResult.Order.OrderId, orderId);
        }

        public GetOrderResponse GetOrderFromService(string customerId, string orderId, out bool success)
        {
            _getOrderResponse = _client.GetOrder(new GetOrderRequest(customerId, orderId));    
            success = _getOrderResponse.GetOrderResult.Message.Contains(PcnIntegrationServicesConstants.OrderValidationMessages.OrderFound);
            return _getOrderResponse;
        }

        private GetOrderResponse GetOrderFromService(string customerId, string orderId)
        {
            _getOrderResponse = _client.GetOrder(new GetOrderRequest(customerId, orderId));
            return _getOrderResponse;
        }

        public void Dispose()
        {
            _client = null;
        }
    }
}
