using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReswareOrderMonitorService.StatusDocumentBuilders;
using Aspose.Words;
using Resware.Entities.Orders;
using ReswareCommon.Constants;
using ReswareOrderMonitorService.eClosingIntegrationService;

namespace Resware.MonitorService.Test.StatusDocumentBuilders.Test
{
    [TestClass]
    public class AssignedAttorneyStatusDocumentBuilderTest
    {
        private DocumentBuilder _documentBuilder;
        private Order _reswareOrder;
        private GetOrderResult _eClosingOrder;
        private AssignedAttorneyStatusDocumentBuilder _assignedAttorneyStatusDocumentBuilder;

        [TestInitialize]
        public void Setup()
        {
            _documentBuilder = new DocumentBuilder();
            _reswareOrder = new Order();
            _eClosingOrder = new GetOrderResult { Order = new OutboundOrder { Borrower = new Person(), Attorneys = new AttorneyInfoForOrder[0], ClosingAttorney = new AttorneyInfoForOrder {Services = new Service[0]} } };
            _assignedAttorneyStatusDocumentBuilder = new AssignedAttorneyStatusDocumentBuilder();
        }

        [TestMethod]
        public void AddBody_should_add_appropriate_lines_to_assigned_attorney_document()
        {
            // Arrange
            _eClosingOrder.Order.OrderId = "123456";

            // Act
            _assignedAttorneyStatusDocumentBuilder.AddBody(_documentBuilder, _reswareOrder, _eClosingOrder);
            
            // Assert
            Assert.IsNotNull(_documentBuilder.Document);
            Assert.IsTrue(_documentBuilder.Document.GetText().Contains("PCN Network Services Confirmation"));
            Assert.IsTrue(_documentBuilder.Document.GetText().Contains("123456"));
        }

        [TestMethod]
        public void AddClosingDueDateTime_should_add_due_date_and_time_to_document()
        {
            // Arrange
            var currentDateTime = DateTime.Now;
            _eClosingOrder.Order.ClosingDate = currentDateTime.ToShortDateString();
            _eClosingOrder.Order.ClosingTime = currentDateTime.ToShortTimeString();

            // Act
            _assignedAttorneyStatusDocumentBuilder.AddClosingDueDateTime(_documentBuilder, _eClosingOrder);

            // Assert
            Assert.IsTrue(_documentBuilder.Document.GetText().Contains(_eClosingOrder.Order.ClosingDate));
            Assert.IsTrue(_documentBuilder.Document.GetText().Contains(_eClosingOrder.Order.ClosingTime));
        }

        [TestMethod]
        public void DetermineAttorneyInfo_should_not_add_any_attorney_info_to_document()
        {
            // Arrange
            _eClosingOrder.Order.Attorneys = new AttorneyInfoForOrder[0];

            // Act
            _assignedAttorneyStatusDocumentBuilder.DetermineAttorneyInfo(_documentBuilder, _eClosingOrder);

            // Assert
            Assert.IsTrue(string.IsNullOrWhiteSpace(_documentBuilder.Document.GetText()));
        }

        [TestMethod]
        public void DetermineAttorneyInfo_should_add_attorney_info_to_document()
        {
            // Arrange
            _eClosingOrder.Order.Attorneys = new [] {new AttorneyInfoForOrder {FirstName = "Bob", Address = new Address(), Services = new [] {new Service {Name = ServiceNameConstants.TitleOpinionLetter}}}};

            // Act
            _assignedAttorneyStatusDocumentBuilder.DetermineAttorneyInfo(_documentBuilder, _eClosingOrder);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(_documentBuilder.Document.GetText()));
            Assert.IsTrue(_documentBuilder.Document.GetText().Contains("Bob"));
        }
    }
}
