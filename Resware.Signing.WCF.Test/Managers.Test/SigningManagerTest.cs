using Adeptive.ResWare.Services;
using Effort;
using Effort.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Data.Context;
using Resware.Data.Signing.Repository;
using ReswareCommon.Messages;
using SigningService.Managers;
using SigningService.Readers;
using SigningService.Utilities;

namespace Resware.Signing.WCF.Test.Managers.Test
{
    [TestClass]
    public class SigningManagerTest
    {
        private SigningReader _signingReader;
        private SigningRepository _signingRepository;
        private ValidIncomingSigningUtility _validIncomingSigningUtility;
        private ISigningManager _signingManager;
        private ReceiveSigningData _receiveSigningData;

        [TestInitialize]
        public void Setup()
        {
            _signingReader = new SigningReader();
            EffortProviderConfiguration.RegisterProvider();
            var connection = DbConnectionFactory.CreateTransient();
            var reswareDbContext = new ReswareDbContext(connection);
            _signingRepository = new SigningRepository(reswareDbContext);
            _validIncomingSigningUtility = new ValidIncomingSigningUtility();
            _signingManager = new SigningManager(_signingReader, _signingRepository, _validIncomingSigningUtility);
            _receiveSigningData = new ReceiveSigningData
            {
                FileNumber = "123456",
                Location = "Home",
                LocationStreet1 = "123 Jump Street",
                LocationCity = "Canonsburg",
                LocationZIP = "15317",
                LocationState = "PA",
                LocationCounty = "Washington"
            };
        }

        [TestMethod]
        public void PlaceSigning_passed_null_value_into_method_should_return_negative_one_with_an_exception_message()
        {
            // Act
            var result = _signingManager.PlaceSigning(null);

            // Assert
            Assert.AreEqual(-1, result.Result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Message));
            Assert.IsTrue(result.Message.Contains("Exception"));
        }

        [TestMethod]
        public void PlaceSigning_passed_signing_data_with_empty_file_number_should_return_zero_and_file_number_is_null_message()
        {
            // Arrange
            _receiveSigningData.FileNumber = string.Empty;

            // Act
            var result = _signingManager.PlaceSigning(_receiveSigningData);

            // Assert
            Assert.AreEqual(0, result.Result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Message));
            Assert.AreEqual(ValidationMessages.FileNumberIsNull, result.Message);
        }

        [TestMethod]
        public void PlaceSigning_passed_signing_data_with_empty_location_should_return_zero_and_location_is_null_message()
        {
            // Arrange
            _receiveSigningData.Location = string.Empty;

            // Act
            var result = _signingManager.PlaceSigning(_receiveSigningData);

            // Assert
            Assert.AreEqual(0, result.Result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Message));
            Assert.AreEqual(ValidationMessages.LocationIsNull, result.Message);
        }

        [TestMethod]
        public void PlaceSigning_passed_valid_signing_data_should_parse_input_and_save_to_repo_return_a_one_with_no_message()
        {
            // Act
            var result = _signingManager.PlaceSigning(_receiveSigningData);

            // Assert
            Assert.AreEqual(1, result.Result);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
        }

        [TestMethod]
        public void PlaceSigning_passed_valid_signing_data_with_signing_party_should_save_to_repo_and_return_a_two_with_no_message()
        {
            // Arrange
            _receiveSigningData.SigningParties = new [] { new ReceiveSigningParty { Name = "Billy Bob", Phone = "1234567890" } };
            
            // Act
            var result = _signingManager.PlaceSigning(_receiveSigningData);

            // Assert
            Assert.AreEqual(2, result.Result);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
        }
    }
}
