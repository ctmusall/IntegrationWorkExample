using System;
using System.Collections.Generic;
using Aspose.Words;
using eClosings.Entities.Attorneys;
using eClosings.Entities.Couriers;
using eClosings.Entities.Persons;
using eClosings.Entities.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Core.Builders.StatusDocument.ClosingCompleted;
using Resware.Entities.Orders;

namespace Resware.MonitorService.Test.StatusDocumentBuilders.Test
{
    [TestClass]
    public class ClosingCompletedStatusDocumentBuilderTest
    {
        private ClosingCompletedStatusDocumentBuilder _closingCompletedStatusDocumentBuilder;
        private DocumentBuilder _documentBuilder;
        private Order _reswareOrder;
        private eClosings.Entities.Orders.Order _eClosingOrder;

        [TestInitialize]
        public void Setup()
        {
            _documentBuilder = new DocumentBuilder();
            _reswareOrder = new Order();
            _eClosingOrder = new eClosings.Entities.Orders.Order() { Couriers = new List<Courier>(), Attorneys = new List<Attorney>()};
            _closingCompletedStatusDocumentBuilder = new ClosingCompletedStatusDocumentBuilder();    
        }

        [TestMethod]
        public void AddBody_should_add_post_closing_body_text()
        {
            // Arrange
            _eClosingOrder = new eClosings.Entities.Orders.Order()
            {
                Borrower = new Person(),
                ClosingDate = DateTime.Now.ToShortDateString(),
                ClosingTime = DateTime.Now.ToShortTimeString(),
                Couriers = new Courier[0],
                Attorneys = new Attorney[0],
                ClosingAttorney = new Attorney { Services = new Service[0]}
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
