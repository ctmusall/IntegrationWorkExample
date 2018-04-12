using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Resware.Data.Context;
using Resware.Data.Signing.Repository;
using Resware.Entities.Orders;
using Resware.Entities.Signings;
using ReswareOrderMonitorService.ActionEvents.Solidifi;
using ReswareOrderMonitorService.Mirth;
using ReswareOrderMonitorService.Utilities.Solidifi;

namespace Resware.MonitorService.Test.ActionEvents.Test.Solidifi.Test
{
    [TestClass]
    public class SolidifiRequestClosingTest
    {
        private SolidifiRequestClosing _solidifiRequestClosing;
        private SigningRepository _signingRepository;
        private ReswareDbContext _reswareDbContext;
        private Mock<IMirthServiceClient> _mirthServiceClientMock;
        private Mock<SolidifiClosingServiceUtility> _solidifiClosingServiceUtilityMock;

        [TestInitialize]
        public void Setup()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _reswareDbContext = new ReswareDbContext(connection);
            _signingRepository = new SigningRepository(_reswareDbContext);
            _mirthServiceClientMock = new Mock<IMirthServiceClient>();
            _solidifiClosingServiceUtilityMock = new Mock<SolidifiClosingServiceUtility>();
            _solidifiRequestClosing = new SolidifiRequestClosing(_signingRepository, _mirthServiceClientMock.Object, _solidifiClosingServiceUtilityMock.Object);
        }

        [TestMethod]
        public void BuildRequestMessage_should_return_new_request_message_with_order_and_signing_fields_mapped()
        {
            // Arrange
            var order = new Order();
            var signing = new Signing();

            // Act
            var result = _solidifiRequestClosing.BuildRequestMessage(order, signing);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.OrderId, order.FileNumber);
            Assert.AreEqual(result.CustomerId, order.CustomerId);
            Assert.AreEqual(result.CustomerContact, "TEAM CLOSINGS");
            Assert.AreEqual(result.LenderName, order.LenderName);
            Assert.AreEqual(result.Product, order.Product);
            Assert.AreEqual(result.CustomerProduct, order.CustomerProduct);
            Assert.AreEqual(result.FileNumber, order.FileNumber);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.OrderRequestedDate));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.OrderRequestedTime));
            Assert.AreEqual(result.DocsToAttorney, "eDoc");
            Assert.AreEqual(result.ClosingAddress1, signing.ClosingAddress);
            Assert.AreEqual(result.ClosingCity, signing.ClosingCity);
            Assert.AreEqual(result.ClosingState, signing.ClosingState);
            Assert.AreEqual(result.ClosingZipCode, signing.ClosingZip);
            Assert.AreEqual(result.ClosingCounty, signing.ClosingCounty);
        }

        [TestMethod]
        public void PerformAction_signing_returned_is_null_should_return_false()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
