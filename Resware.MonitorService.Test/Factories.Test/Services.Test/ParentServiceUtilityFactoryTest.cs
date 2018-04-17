using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Core.Services.Factories.ParentServiceUtilities;
using Resware.Core.Services.Factories.ServiceUtilities;

namespace Resware.MonitorService.Test.Factories.Test.Services.Test
{
    [TestClass]
    public class ParentServiceUtilityFactoryTest
    {
        private ParentServiceUtilityFactory _parentServiceUtilityFactory;

        [TestInitialize]
        public void Setup()
        {
            _parentServiceUtilityFactory = new ParentServiceUtilityFactory();
        }

        [TestMethod]
        public void ResolveServiceUtilityFactory_client_id_does_not_match_should_return_null()
        {
            // Act
            var result = _parentServiceUtilityFactory.ResolveServiceUtilityFactory(0);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ResolveServiceUtilityFactory_client_id_matches_solidifi_should_return_solidifi_service_utility_factory()
        {
            // Act
            var result = _parentServiceUtilityFactory.ResolveServiceUtilityFactory(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SolidifiServiceUtilityFactory));
        }
    }
}
