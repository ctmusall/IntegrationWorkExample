using eClosings.Core.Utilities;
using eClosings.Mirth.Clients;
using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Resware.Core.ActionEvent.RequestTitleOpinion.ActionEvents;
using Resware.Core.Services.Utilities.ServiceUtilities.TitleOpinionService;
using Resware.Data.Context;
using Resware.Data.Signing.Repository;
using Resware.Entities.Orders;
using Resware.Entities.Signings;
using ReswareCommon.Constants;

namespace Resware.MonitorService.Test.ActionEvents.Test.Solidifi.Test
{
    [TestClass]
    public class SolidifiRequestTitleOpinionTest
    {
        private Order _order;
        private Signing _signing;
        private SolidifiRequestTitleOpinion _solidifiRequestTitleOpinion;
        private Mock<IDateTimeUtility> _dateTimeUtilityMock;
        private SigningRepository _signingRepository;
        private ReswareDbContext _reswareDbContext;
        private Mock<MirthServiceClient> _mirthServiceClientMock;
        private Mock<SolidifiTitleOpinionServiceUtility> _solidifiTitleOpinionServiceUtilityMock;

        [TestInitialize]
        public void Setup()
        {
            _order = new Order { FileNumber = "123456" };
            _signing = new Signing();
            var connection = DbConnectionFactory.CreateTransient();
            _reswareDbContext = new ReswareDbContext(connection);
            _signingRepository = new SigningRepository(_reswareDbContext);
            _mirthServiceClientMock = new Mock<MirthServiceClient>();
            _solidifiTitleOpinionServiceUtilityMock = new Mock<SolidifiTitleOpinionServiceUtility>();
            _dateTimeUtilityMock = new Mock<IDateTimeUtility>();
            _solidifiRequestTitleOpinion = new SolidifiRequestTitleOpinion(_signingRepository, _mirthServiceClientMock.Object, _solidifiTitleOpinionServiceUtilityMock.Object, _dateTimeUtilityMock.Object);    
        }

        [TestMethod]
        public void BuildRequestMessage_should_return_request_message_with_order_and_signing_fields_mapped()
        {
            // Act
            var result = _solidifiRequestTitleOpinion.BuildRequestMessage(_order, _signing);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual($"{_order.FileNumber}-T", result.OrderId);
            Assert.AreEqual(_order.CustomerId, result.CustomerId);
            Assert.AreEqual(CustomerContactConstants.KristenMiller, result.CustomerContact);
            Assert.AreEqual(_order.LenderName, result.LenderName);
            Assert.AreEqual(ProductNameConstants.EClosingsProductNames.IntSearchOpinion, result.Product);
            Assert.AreEqual(_order.FileNumber, result.FileNumber);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.OrderRequestedDate));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.OrderRequestedTime));
            Assert.AreEqual(_signing.ClosingAddress, result.ClosingAddress1);
            Assert.AreEqual(_signing.ClosingCity, result.ClosingCity);
            Assert.AreEqual(_signing.ClosingState, result.ClosingState);
            Assert.AreEqual(_signing.ClosingZip, result.ClosingZipCode);
            Assert.AreEqual(_signing.ClosingCounty, result.ClosingCounty);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ClosingDate));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ClosingTime));
        }
    }
}
