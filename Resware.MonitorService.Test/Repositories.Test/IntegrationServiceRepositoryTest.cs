using eClosings.Data.eClosingsIntegrationService;
using eClosings.Data.IntegrationService.Repository;
using eClosings.Data.Readers.OrderReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Resware.MonitorService.Test.Repositories.Test
{
    [TestClass]
    public class IntegrationServiceRepositoryTest
    {
        private IntegrationServiceRepository _integrationServiceRepository;
        private Mock<IIntegrationService> _integrationServiceClientMock;
        private EClosingOrderReader _eClosingOrderReader;

        [TestInitialize]
        public void Setup()
        {
            _integrationServiceClientMock = new Mock<IIntegrationService>();
            _eClosingOrderReader = new EClosingOrderReader();
            _integrationServiceRepository = new IntegrationServiceRepository(_integrationServiceClientMock.Object, _eClosingOrderReader);
        }

        [TestMethod]
        public void GetOrder_should_return_empty_get_order_result()
        {
            // Arrange
            _integrationServiceClientMock.Setup(isc => isc.GetOrder("123", "123456")).Returns(new GetOrderResult {Outcome = OutcomeEnum.Fail});

            // Act
            var result = _integrationServiceRepository.GetOrder("123", "123456");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetOrder_should_return_successful_get_order_result()
        {
            // Arrange
            _integrationServiceClientMock.Setup(isc => isc.GetOrder("123", "123456")).Returns(new GetOrderResult {Outcome = OutcomeEnum.Success, Order = new OutboundOrder {CustomerId = "123", FileNumber = "123456", OrderId = "123456"}});
            
            // Act
            var result = _integrationServiceRepository.GetOrder("123", "123456");

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
