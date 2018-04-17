using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.Readers;

namespace Resware.MonitorService.Test.Readers.Test
{
    [TestClass]
    public class EClosingOrderReaderTest
    {
        private IEClosingOrderReader _eClosingOrderReader;

        [TestInitialize]
        public void Setup()
        {
            _eClosingOrderReader = new EClosingOrderReader();
        }

        [TestMethod]
        public void MapEClosingOrder_should_return_null_get_order_result_is_null()
        {
            // Act
            var result = _eClosingOrderReader.MapEClosingOrder(null);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MapEClosingOrder_should_return_new_eclosing_order_result_with_mapped_eclosing_attorneys_services_couriers_borrowers_and_addresses()
        {
            // Arrange
            var getOrderResult = new GetOrderResult
            {
                Order = new OutboundOrder
                {
                    AdjournedReason = "Test",
                    Attorneys = new AttorneyInfoForOrder[0],
                    BorrowerContacted = DateTime.Now,
                    BorrowerContactedStatusSent = DateTime.Now,
                    CancelledReason = "Test",
                    ClosingAttorney = new AttorneyInfoForOrder(),
                    Couriers = new Courier[0],
                    CustomerName = "Test",
                    NotClosedReason = "Test",
                    Notes = "Test",
                    Status = "Test",
                    TotalBillRate = 12,
                    UnableReason = "Test",
                    Borrower = new AttorneyInfoForOrder(),
                    CoBorrower = new AttorneyInfoForOrder(),
                    ClosingAddress = new Address(),
                    ClosingDate = DateTime.Now.ToShortDateString(),
                    ClosingTime = DateTime.Now.ToShortTimeString(),
                    ClosingLocation = "Test",
                    CustomerId = "23456",
                    CustomerContact = "PERSON",
                    DeliveryMethod = "Test",
                    FileNumber = "123456",
                    LenderName = "Test",
                    LoanNumber = "233232",
                    OrderId = "123456",
                    Product = "Refinance",
                    RequestedClosingTime = DateTime.Now.ToShortTimeString(),
                    RequestedClosingDate = DateTime.Now.ToShortDateString()
                }
            };

            // Act
            var result = _eClosingOrderReader.MapEClosingOrder(getOrderResult);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EClosingOrder));
            Assert.AreEqual(getOrderResult.Order.FileNumber, result.FileNumber);
            Assert.AreEqual(getOrderResult.Order.OrderId, result.OrderId);
        }
    }
}
