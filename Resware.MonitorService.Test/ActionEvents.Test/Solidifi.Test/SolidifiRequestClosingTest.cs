using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Resware.Data.Context;
using Resware.Data.Signing.Repository;
using Resware.Entities.Orders;
using Resware.Entities.Signings;
using ReswareCommon.Constants;
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
        private Order _order;
        private Signing _signing;

        [TestInitialize]
        public void Setup()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _reswareDbContext = new ReswareDbContext(connection);
            _signingRepository = new SigningRepository(_reswareDbContext);
            _mirthServiceClientMock = new Mock<IMirthServiceClient>();
            _solidifiClosingServiceUtilityMock = new Mock<SolidifiClosingServiceUtility>();
            _solidifiRequestClosing = new SolidifiRequestClosing(_signingRepository, _mirthServiceClientMock.Object, _solidifiClosingServiceUtilityMock.Object);
            _order = new Order();
            _signing = new Signing();
        }

        [TestMethod]
        public void BuildRequestMessage_should_return_new_request_message_with_order_and_signing_fields_mapped()
        {
            // Act
            var result = _solidifiRequestClosing.BuildRequestMessage(_order, _signing);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.OrderId, _order.FileNumber);
            Assert.AreEqual(result.CustomerId, _order.CustomerId);
            Assert.AreEqual(result.CustomerContact, CustomerContactConstants.TeamClosings);
            Assert.AreEqual(result.LenderName, _order.LenderName);
            Assert.AreEqual(result.Product, _order.Product);
            Assert.AreEqual(result.CustomerProduct, _order.CustomerProduct);
            Assert.AreEqual(result.FileNumber, _order.FileNumber);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.OrderRequestedDate));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.OrderRequestedTime));
            Assert.AreEqual(result.DocsToAttorney, ProductNameConstants.EClosingsProductNames.EDoc);
            Assert.AreEqual(result.ClosingAddress1, _signing.ClosingAddress);
            Assert.AreEqual(result.ClosingCity, _signing.ClosingCity);
            Assert.AreEqual(result.ClosingState, _signing.ClosingState);
            Assert.AreEqual(result.ClosingZipCode, _signing.ClosingZip);
            Assert.AreEqual(result.ClosingCounty, _signing.ClosingCounty);
        }

        [TestMethod]
        public void PerformAction_signing_for_resware_order_returned_is_null_should_return_false()
        {
            // Act
            var result = _solidifiRequestClosing.PerformAction(_order);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void PerformAction_retrieve_signing_information_for_order_and_order_is_not_sent_successfully_to_mirth_should_return_false()
        {
            // Arrange
            _order.FileNumber = "123456";
            _signing.FileNumber = "123456";

            _reswareDbContext.Signings.Add(_signing);
            _reswareDbContext.SaveChanges();

            // Act
            var result = _solidifiRequestClosing.PerformAction(_order);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void PerformAction_retrieve_signing_information_for_order_and_order_is_sent_successfully_to_mirth_should_return_true()
        {
            // Arrange
            _order.FileNumber = "123456";
            _signing.FileNumber = "123456";

            _reswareDbContext.Signings.Add(_signing);
            _reswareDbContext.SaveChanges();

            _mirthServiceClientMock.Setup(m => m.SendMessageToMirth(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(true);

            // Act
            var result = _solidifiRequestClosing.PerformAction(_order);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
