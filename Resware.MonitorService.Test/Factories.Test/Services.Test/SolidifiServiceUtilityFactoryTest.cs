using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Utilities.Solidifi;

namespace Resware.MonitorService.Test.Factories.Test.Services.Test
{
    [TestClass]
    public class SolidifiServiceUtilityFactoryTest
    {
        private SolidifiServiceUtilityFactory _solidifiServiceUtilityFactory;

        [TestInitialize]
        public void Setup()
        {
            _solidifiServiceUtilityFactory = new SolidifiServiceUtilityFactory();
        }

        [TestMethod]
        public void ResolveServiceUtility_service_utility_type_matches_closing_order_type_return_solidifi_closing_service_utility()
        {
            // Act
            var result = _solidifiServiceUtilityFactory.ResolveServiceUtility(OrderTypeEnum.Closing);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SolidifiClosingServiceUtility));
        }

        [TestMethod]
        public void ResolveServiceUtility_service_utility_type_matches_title_opinion_order_type_return_solidifi_title_opinion_service_utility()
        {
            // Act
            var result = _solidifiServiceUtilityFactory.ResolveServiceUtility(OrderTypeEnum.TitleOpinion);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SolidifiTitleOpinionServiceUtility));
        }

        [TestMethod]
        public void ResolveServiceUtility_service_utility_type_matches_docprep_order_type_return_solidifi_docprep_service_utility()
        {
            // Act
            var result = _solidifiServiceUtilityFactory.ResolveServiceUtility(OrderTypeEnum.DocPrep);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SolidifiDocPrepServiceUtility));
        }
    }
}
