using System;
using System.Collections.Generic;
using Aspose.Words;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.StatusDocumentBuilders;

namespace Resware.MonitorService.Test.StatusDocumentBuilders.Test
{
    [TestClass]
    public class ClosingCompletedStatusDocumentBuilderTest
    {
        private ClosingCompletedStatusDocumentBuilder _closingCompletedStatusDocumentBuilder;
        private DocumentBuilder _documentBuilder;
        private Order _reswareOrder;
        private EClosingOrder _eClosingOrder;

        [TestInitialize]
        public void Setup()
        {
            _documentBuilder = new DocumentBuilder();
            _reswareOrder = new Order();
            _eClosingOrder = new EClosingOrder() { Couriers = new List<EClosingCourier>(), Attorneys = new List<EClosingAttorney>()};
            _closingCompletedStatusDocumentBuilder = new ClosingCompletedStatusDocumentBuilder();    
        }

        [TestMethod]
        public void AddBody_should_add_post_closing_body_text()
        {
            // Arrange
            _eClosingOrder = new EClosingOrder
            {
                Borrower = new EClosingPerson(),
                ClosingDate = DateTime.Now.ToShortDateString(),
                ClosingTime = DateTime.Now.ToShortTimeString(),
                Couriers = new EClosingCourier[0],
                Attorneys = new EClosingAttorney[0],
                ClosingAttorney = new EClosingAttorney { Services = new EClosingService[0]}
            };

            // Act
            _closingCompletedStatusDocumentBuilder.AddBody(_documentBuilder, _reswareOrder, _eClosingOrder);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(_documentBuilder.Document.GetText()));
            Assert.IsTrue(_documentBuilder.Document.GetText().Contains("POST CLOSING CLIENT NOTIFICATION"));
        }

        [TestMethod]
        public void BuildDocument_should_return_new_document_with_order_info()
        {
            // Act
            var result = _closingCompletedStatusDocumentBuilder.BuildDocument(_reswareOrder, _eClosingOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.GetText()));
        }
    }
}
