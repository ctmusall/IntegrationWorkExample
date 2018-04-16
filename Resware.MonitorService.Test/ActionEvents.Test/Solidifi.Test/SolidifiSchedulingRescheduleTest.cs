using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Resware.Data.Context;
using Resware.Data.Signing.Repository;
using Resware.Entities.Orders;
using Resware.Entities.Signings;
using ReswareOrderMonitorService.ActionEvents.Solidifi;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Mirth;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.Utilities.Solidifi;

namespace Resware.MonitorService.Test.ActionEvents.Test.Solidifi.Test
{
    [TestClass]
    public class SolidifiSchedulingRescheduleTest
    {
        private Order _order;
        private Signing _signing;
        private SolidifiSchedulingReschedule _solidifiSchedulingReschedule;
        private SigningRepository _signingRepository;
        private ReswareDbContext _reswareDbContext;
        private Mock<IMirthServiceClient> _mirthServiceClientMock;
        private Mock<IIntegrationServiceRepository> _integrationServiceRepositoryMock;
        private Mock<SolidifiClosingServiceUtility> _solidifiClosingServiceUtility;

        [TestInitialize]
        public void Setup()
        {
            _order = new Order {FileNumber = "123456"};
            _signing = new Signing {FileNumber = "123456"};
            var connection = DbConnectionFactory.CreateTransient();
            _reswareDbContext = new ReswareDbContext(connection);
            _signingRepository = new SigningRepository(_reswareDbContext);
            _mirthServiceClientMock = new Mock<IMirthServiceClient>();
            _solidifiClosingServiceUtility = new Mock<SolidifiClosingServiceUtility>();
            _integrationServiceRepositoryMock = new Mock<IIntegrationServiceRepository>();
            _solidifiSchedulingReschedule = new SolidifiSchedulingReschedule(_solidifiClosingServiceUtility.Object, _integrationServiceRepositoryMock.Object, _signingRepository, _mirthServiceClientMock.Object);   
        }

        [TestMethod]
        public void PerformAction_should_add_to_the_incoming_order_notes_order_did_not_exist_in_eclosings_and_perform_the_request_closing_perform_action_method_should_return_true()
        {
            // Arrange
            _integrationServiceRepositoryMock.Setup(isr => isr.GetOrder(It.IsAny<string>(), It.IsAny<string>())).Returns(new EClosingOrder());
            _reswareDbContext.Signings.Add(_signing);
            _reswareDbContext.SaveChanges();
            _mirthServiceClientMock.Setup(m => m.SendMessageToMirth(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(true);

            // Act
            var result = _solidifiSchedulingReschedule.PerformAction(_order);
            
            // Assert
            Assert.IsTrue(result);
        }
    }
}
