using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Core.ActionEvent.Factories.ActionEvents;
using Resware.Core.ActionEvent.RequestClosing.ActionEvents;
using Resware.Core.ActionEvent.RequestDocPrep.ActionEvents;
using Resware.Core.ActionEvent.RequestFundingAuth.ActionEvents;
using Resware.Core.ActionEvent.RequestReschedule.ActionEvents;
using Resware.Core.ActionEvent.RequestTitleOpinion.ActionEvents;
using Resware.Core.Services.Factories.ServiceUtilities;
using ReswareCommon.Constants.Solidifi;

namespace Resware.MonitorService.Test.Factories.Test.ActionEvents.Test
{
    [TestClass]
    public class SolidifiActionEventFactoryTest
    {
        private SolidifiActionEventFactory _solidifiActionEventFactory;

        [TestInitialize]
        public void Setup()
        {
            _solidifiActionEventFactory = new SolidifiActionEventFactory(new SolidifiServiceUtilityFactory());
        }

        [TestMethod]
        public void ResolveActionEvent_action_event_code_does_not_match_solidifi_action_event_constant_should_return_null()
        {
            // Act
            var result = _solidifiActionEventFactory.ResolveActionEvent(string.Empty);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ResolveActionEvent_action_event_code_matches_solidifi_reschedule_closing_should_return_scheduling_reschedule()
        {
            // Act
            var result = _solidifiActionEventFactory.ResolveActionEvent(SolidifiActionEventConstants.RescheduleClosing);

            // Assert
            Assert.AreEqual(typeof(SolidifiRequestReschedule), result.GetType());
        }

        [TestMethod]
        public void ResolveActionEvent_action_event_code_matches_solidifi_request_closing_should_return_request_closing()
        {
            // Act
            var result = _solidifiActionEventFactory.ResolveActionEvent(SolidifiActionEventConstants.RequestClosing);

            // Assert
            Assert.AreEqual(typeof(SolidifiRequestClosing), result.GetType());
        }

        [TestMethod]
        public void ResolveActionEvent_action_event_code_matches_solidifi_request_title_opinion_should_return_request_title_opinion()
        {
            // Act
            var result = _solidifiActionEventFactory.ResolveActionEvent(SolidifiActionEventConstants.RequestTitleOpinion);

            // Assert
            Assert.AreEqual(typeof(SolidifiRequestTitleOpinion), result.GetType());
        }

        [TestMethod]
        public void ResolveActionEvent_action_event_code_matches_solidifi_request_doc_prep_should_return_request_doc_prep()
        {
            // Act
            var result = _solidifiActionEventFactory.ResolveActionEvent(SolidifiActionEventConstants.RequestDocPrep);

            // Assert
            Assert.AreEqual(typeof(SolidifiRequestDocPrep), result.GetType());
        }

        [TestMethod]
        public void ResolveActionEvent_action_event_code_matches_solidifi_funding_auth_should_return_funding_auth()
        {
            // Act
            var result = _solidifiActionEventFactory.ResolveActionEvent(SolidifiActionEventConstants.FundingAuth);

            // Assert
            Assert.AreEqual(typeof(RequestFundingAuth), result.GetType());
        }
    }
}
