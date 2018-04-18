using eClosings.Data.IntegrationService.Repository;
using eClosings.Entities.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Resware.Core.ActionEvent.Factories.ClientCompletedActionEvents;
using Resware.Core.Status.Factories.StatusSender.ClosingStatus;
using Resware.Core.Status.Factories.StatusSender.DocPrepStatus;
using Resware.Core.Status.Factories.StatusSender.TitleOpinion;
using ReswareCommon.Constants.Solidifi;

namespace Resware.MonitorService.Test.Factories.Test.CompletedActionEvents.Test.Solidifi.Test
{
    [TestClass]
    public class SolidifiCompletedActionEventFactoryTest
    {
        private Mock<IIntegrationServiceRepository> _integrationServiceRepositoryMock;
        private SolidifiCompletedActionEventFactory _solidifiCompletedActionEventFactory;
        private const string CustomerId = "1";
        private const string FileNumber = "123456";

        [TestInitialize]
        public void Setup()
        {
            _integrationServiceRepositoryMock = new Mock<IIntegrationServiceRepository>();
            _integrationServiceRepositoryMock.Setup(isr => isr.GetOrder(CustomerId, FileNumber)).Returns(new Order());
            _solidifiCompletedActionEventFactory = new SolidifiCompletedActionEventFactory(_integrationServiceRepositoryMock.Object);
        }

        [TestMethod]
        public void ResolveCompletedActionEventStatusSenderFactory_action_event_code_does_not_exist_should_return_null()
        {
            // Act
            var result = _solidifiCompletedActionEventFactory.ResolveCompletedActionEventStatusSenderFactory(string.Empty, string.Empty, string.Empty);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ResolveCompletedActionEventStatusSenderFactory_action_event_code_matches_solidifi_request_closing_should_return_type_solidifi_closing_status_sender_factory()
        {
            // Act
            var result = _solidifiCompletedActionEventFactory.ResolveCompletedActionEventStatusSenderFactory(SolidifiActionEventConstants.RequestClosing, CustomerId, FileNumber);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SolidifiClosingStatusSenderFactory));
        }

        [TestMethod]
        public void ResolveCompletedActionEventStatusSenderFactory_action_event_code_matches_solidifi_request_title_opinion_should_return_type_solidifi_title_opinion_status_sender_factory()
        {
            // Act
            var result = _solidifiCompletedActionEventFactory.ResolveCompletedActionEventStatusSenderFactory(SolidifiActionEventConstants.RequestTitleOpinion, CustomerId, FileNumber);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SolidifiTitleOpinionStatusSenderFactory));
        }

        [TestMethod]
        public void ResolveCompletedActionEventStatusSenderFactory_action_event_code_matches_solidifi_request_doc_prep_should_return_type_solidifi_doc_prep_status_sender_factory()
        {
            // Act
            var result = _solidifiCompletedActionEventFactory.ResolveCompletedActionEventStatusSenderFactory(SolidifiActionEventConstants.RequestDocPrep, CustomerId, FileNumber);

            // Assert    
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SolidifiDocPrepStatusSenderFactory));
        }

    }
}
