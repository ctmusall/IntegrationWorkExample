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
using ReswareOrderMonitorService.Utilities;
using ReswareOrderMonitorService.Utilities.Solidifi;

namespace Resware.MonitorService.Test.ActionEvents.Test.Solidifi.Test
{
    [TestClass]
    public class SolidifiRequestDocPrepTest
    {
        private SolidifiRequestDocPrep _solidifiRequestDocPrep;
        private SigningRepository _signingRepository;
        private ReswareDbContext _reswareDbContext;
        private Mock<IMirthServiceClient> _mirthServiceClientMock;
        private Mock<SolidifiClosingServiceUtility> _solidifiClosingServiceUtilityMock;
        private Mock<IDateTimeUtility> _dateTimeUtilityMock;
        private Order _order;
        private Signing _signing;

        [TestInitialize]
        public void Setup()
        {
            _order = new Order { FileNumber = "123456" };
            _signing = new Signing();
            var connection = DbConnectionFactory.CreateTransient();
            _reswareDbContext = new ReswareDbContext(connection);
            _signingRepository = new SigningRepository(_reswareDbContext);
            _mirthServiceClientMock = new Mock<IMirthServiceClient>();
            _solidifiClosingServiceUtilityMock = new Mock<SolidifiClosingServiceUtility>();
            _dateTimeUtilityMock = new Mock<IDateTimeUtility>();
            _solidifiRequestDocPrep = new SolidifiRequestDocPrep(_signingRepository, _mirthServiceClientMock.Object, _solidifiClosingServiceUtilityMock.Object, _dateTimeUtilityMock.Object);
        }

        [TestMethod]
        public void BuildRequestMessage_should_return_new_request_message_with_order_and_signing_fields_mapped()
        {
            // Act
            var result = _solidifiRequestDocPrep.BuildRequestMessage(_order, _signing);

            // Assert
            Assert.AreEqual($"{_order.FileNumber}-D", result.OrderId);
            Assert.AreEqual(_order.CustomerId, result.CustomerId);
            Assert.AreEqual("DOC DEED", result.CustomerContact);
            Assert.AreEqual(_order.LenderName, result.LenderName);
            Assert.AreEqual(_order.CustomerProduct, result.CustomerProduct);
            Assert.AreEqual(_order.FileNumber, result.FileNumber);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.OrderRequestedDate));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.OrderRequestedTime));
            Assert.AreEqual(_signing.ClosingAddress, result.ClosingAddress1);
            Assert.AreEqual(_signing.ClosingCity, result.ClosingCity);
            Assert.AreEqual(_signing.ClosingState, result.ClosingState);
            Assert.AreEqual(_signing.ClosingZip, result.ClosingZipCode);
            Assert.AreEqual(_signing.ClosingCounty, result.ClosingCounty);
            Assert.AreEqual(ProductNameConstants.EClosingsProductNames.DeedPrepExternal, result.Product);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ClosingDate));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ClosingTime));
        }
    }
}
