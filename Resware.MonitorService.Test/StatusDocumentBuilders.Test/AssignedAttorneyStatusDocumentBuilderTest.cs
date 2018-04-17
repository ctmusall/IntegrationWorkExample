using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aspose.Words;
using eClosings.Entities.Addresses;
using eClosings.Entities.Attorneys;
using eClosings.Entities.Persons;
using eClosings.Entities.Services;
using Resware.Core.Builders.StatusDocument.AssignedAttorney;
using Resware.Entities.Orders;
using ReswareCommon.Constants;

namespace Resware.MonitorService.Test.StatusDocumentBuilders.Test
{
    [TestClass]
    public class AssignedAttorneyStatusDocumentBuilderTest
    {
        private DocumentBuilder _documentBuilder;
        private Order _reswareOrder;
        private eClosings.Entities.Orders.Order _eClosingOrder;
        private AssignedAttorneyStatusDocumentBuilder _assignedAttorneyStatusDocumentBuilder;

        [TestInitialize]
        public void Setup()
        {
            _documentBuilder = new DocumentBuilder();
            _reswareOrder = new Order();
            _eClosingOrder = new eClosings.Entities.Orders.Order { Borrower = new Person(), Attorneys = new Attorney[0], ClosingAttorney = new Attorney {Services = new Service[0]}};
            _assignedAttorneyStatusDocumentBuilder = new AssignedAttorneyStatusDocumentBuilder();
        }

        [TestMethod]
        public void AddBody_should_add_appropriate_lines_to_assigned_attorney_document()
        {
            // Arrange
            _eClosingOrder.OrderId = "123456";

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
            _eClosingOrder.ClosingDate = currentDateTime.ToShortDateString();
            _eClosingOrder.ClosingTime = currentDateTime.ToShortTimeString();

            // Act
            _assignedAttorneyStatusDocumentBuilder.AddClosingDueDateTime(_documentBuilder, _eClosingOrder);

            // Assert
            Assert.IsTrue(_documentBuilder.Document.GetText().Contains(_eClosingOrder.ClosingDate));
            Assert.IsTrue(_documentBuilder.Document.GetText().Contains(_eClosingOrder.ClosingTime));
        }

        [TestMethod]
        public void DetermineAttorneyInfo_should_not_add_any_attorney_info_to_document()
        {
            // Arrange
            _eClosingOrder.Attorneys = new Attorney[0];

            // Act
            _assignedAttorneyStatusDocumentBuilder.DetermineAttorneyInfo(_documentBuilder, _eClosingOrder);

            // Assert
            Assert.IsTrue(string.IsNullOrWhiteSpace(_documentBuilder.Document.GetText()));
        }

        [TestMethod]
        public void DetermineAttorneyInfo_should_add_attorney_info_to_document()
        {
            // Arrange
            _eClosingOrder.Attorneys = new [] {new Attorney { FirstName = "Bob", Address = new Address(), Services = new [] {new Service {Name = ServiceNameConstants.TitleOpinionLetter}}}};

            // Act
            _assignedAttorneyStatusDocumentBuilder.DetermineAttorneyInfo(_documentBuilder, _eClosingOrder);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(_documentBuilder.Document.GetText()));
            Assert.IsTrue(_documentBuilder.Document.GetText().Contains("Bob"));
        }

        [TestMethod]
        public void BuildDocument_should_return_new_document_with_order_info()
        {
            // Act
            var result = _assignedAttorneyStatusDocumentBuilder.BuildDocument(_reswareOrder, _eClosingOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.GetText()));
        }
    }
}
