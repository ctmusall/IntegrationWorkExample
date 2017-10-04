using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCN_Integration.Services;

namespace PCN_Integration.Services
{
  public class PcnWebServiceInvoker : IDisposable
  {
    private string _orderFoundValidationString = "Order found";
    private IIntegrationService _client;
    private GetOrderResult _getOrderResult;
    private GetOrderResponse _getOrderResponse;

    public PcnWebServiceInvoker()
    {            
      _client = new IntegrationServiceClient();
    }

    public bool OrderExists(string customerId, string orderId)
    {
      var result = GetOrderFromService(customerId, orderId);
      return result.GetOrderResult.Message == _orderFoundValidationString && result.GetOrderResult.Order.OrderId == orderId;
    }

    public OrderMessage GetOrder(string customerId, string orderId)
    {
      bool success;
      var getOrderFromService = GetOrderFromService(customerId, orderId, out success);

      if (!success) return new OrderMessage();

      var orderResult = getOrderFromService.GetOrderResult.Order;

      return new OrderMessage()
      {
        orderId = orderResult.OrderId,
        customerId = orderResult.CustomerId,
        customerContact = orderResult.CustomerContact,
        lenderName = orderResult.LenderName,
        borrowerFirstName = orderResult.Borrower.FirstName,
        borrowerLastName = orderResult.Borrower.LastName,
        coborrowerFirstName = orderResult.CoBorrower.FirstName,
        coborrowerLastName = orderResult.CoBorrower.LastName,
        product = orderResult.Product,
        fileNumber = orderResult.FileNumber,
        orderRequestedDate = orderResult.RequestedClosingDate,
        orderRequestedTime = orderResult.RequestedClosingTime,
        closingDate = orderResult.ClosingDate,
        closingTime = orderResult.ClosingTime,
        closingLocation = orderResult.ClosingLocation,
        borrowerAddress1 = orderResult.Borrower.Address.Address1,
        borrowerAddress2 = orderResult.Borrower.Address.Address2,
        borrowerAddress3 = orderResult.Borrower.Address.Address3,
        borrowerCity = orderResult.Borrower.Address.City,
        borrowerState = orderResult.Borrower.Address.State,
        borrowerZipCode = orderResult.Borrower.Address.ZipCode,
        borrowerCounty = orderResult.Borrower.Address.County,
        borrowerPhone1 = orderResult.Borrower.HomePhone,
        borrowerPhone2 = orderResult.Borrower.CellPhone,
        closingAddress1 = orderResult.ClosingAddress.Address1,
        closingAddress2 = orderResult.ClosingAddress.Address2,
        closingAddress3 = orderResult.ClosingAddress.Address3,
        closingCity = orderResult.ClosingAddress.City,
        closingState = orderResult.ClosingAddress.State,
        closingZipCode = orderResult.ClosingAddress.ZipCode,
        closingCounty = orderResult.ClosingAddress.County
      };
    }

    public GetOrderResponse GetOrderFromService(string customerId, string orderId, out bool success)
    {
      _getOrderResponse = _client.GetOrder(new GetOrderRequest(customerId, orderId));
      success = _getOrderResult.Message.Contains(_orderFoundValidationString);

      //var foo = _getOrderResult

      return _getOrderResponse;
    }

    private GetOrderResponse GetOrderFromService(string customerId, string orderId)
    {
      _getOrderResponse = _client.GetOrder(new GetOrderRequest(customerId, orderId));
      return _getOrderResponse;
    }

    public void Dispose()
    {
      _getOrderResult = null;
      _client = null;
    }
  }
}
