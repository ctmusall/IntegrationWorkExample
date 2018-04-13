using System.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.ActionEvents;
using ReswareOrderMonitorService.Utilities;

namespace Resware.MonitorService.Test.ActionEvents.Test
{
    [TestClass]
    public class FundingAuthTest
    {
        private Mock<IFundingAuthMailUtility> _fundingAuthMailUtilityMock;
        private FundingAuth _fundingAuth;
        private Order _order;

        [TestInitialize]
        public void Setup()
        {
            _order = new Order {FileNumber = "123456"};
            _fundingAuthMailUtilityMock = new Mock<IFundingAuthMailUtility>();
            _fundingAuth = new FundingAuth(_fundingAuthMailUtilityMock.Object);    
        }

        [TestMethod]
        public void PerformAction_should_build_mail_message_and_return_true()
        {
            // Arrange
            _fundingAuthMailUtilityMock.Setup(fau => fau.BuildFundingAuthMailMessage(_order)).Returns(new MailMessage());
            _fundingAuthMailUtilityMock.Setup(fau => fau.SendFundingAuthMailMessage(It.IsAny<MailMessage>())).Returns(true);

            // Act
            var result = _fundingAuth.PerformAction(_order);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
