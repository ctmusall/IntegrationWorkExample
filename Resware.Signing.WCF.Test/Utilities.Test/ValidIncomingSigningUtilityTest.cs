using Adeptive.ResWare.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReswareCommon.Messages;
using SigningService.Utilities;

namespace Resware.Signing.WCF.Test.Utilities.Test
{
    [TestClass]
    public class ValidIncomingSigningUtilityTest
    {
        private ValidIncomingSigningUtility _validIncomingSigningUtility;
        private ReceiveSigningData _receiveSigningData;

        [TestInitialize]
        public void Setup()
        {
            _validIncomingSigningUtility = new ValidIncomingSigningUtility();
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
        public void IsIncomingSigningDataValid_should_return_a_valid_result()
        {
            // Act
            var result = _validIncomingSigningUtility.IsIncomingSigningDataValid(_receiveSigningData);

            // Assert
            Assert.IsTrue(result.Valid);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
        }

        [TestMethod]
        public void IsIncomingSigningDataValid_should_return_an_invalid_result_file_number_is_missing()
        {
            // Arrange
            _receiveSigningData.FileNumber = string.Empty;

            // Act
            var result = _validIncomingSigningUtility.IsIncomingSigningDataValid(_receiveSigningData);

            // Assert
            Assert.IsFalse(result.Valid);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Message));
            Assert.AreEqual(ValidationMessages.FileNumberIsNull, result.Message);
        }

        [TestMethod]
        public void IsIncomingSigningDataValid_should_return_an_invalid_result_location_is_missing()
        {
            // Arrange
            _receiveSigningData.Location = string.Empty;

            // Act
            var result = _validIncomingSigningUtility.IsIncomingSigningDataValid(_receiveSigningData);

            // Assert
            Assert.IsFalse(result.Valid);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Message));
            Assert.AreEqual(ValidationMessages.LocationIsNull, result.Message);
        }

        [TestMethod]
        public void IsIncomingSigningDataValid_should_return_an_invalid_result_location_street_is_missing()
        {
            // Arrange
            _receiveSigningData.LocationStreet1 = string.Empty;

            // Act
            var result = _validIncomingSigningUtility.IsIncomingSigningDataValid(_receiveSigningData);

            // Assert
            Assert.IsFalse(result.Valid);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Message));
            Assert.AreEqual(ValidationMessages.LocationStreet1IsNull, result.Message);
        }

        [TestMethod]
        public void IsIncomingSigningDataValid_should_return_an_invalid_result_location_city_is_missing()
        {
            // Arrange
            _receiveSigningData.LocationCity = string.Empty;

            // Act
            var result = _validIncomingSigningUtility.IsIncomingSigningDataValid(_receiveSigningData);

            // Assert
            Assert.IsFalse(result.Valid);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Message));
            Assert.AreEqual(ValidationMessages.LocationCityIsNull, result.Message);
        }

        [TestMethod]
        public void IsIncomingSigningDataValid_should_return_an_invalid_result_location_zip_is_missing()
        {
            // Arrange
            _receiveSigningData.LocationZIP = string.Empty;

            // Act
            var result = _validIncomingSigningUtility.IsIncomingSigningDataValid(_receiveSigningData);

            // Assert
            Assert.IsFalse(result.Valid);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Message));
            Assert.AreEqual(ValidationMessages.LocationZipIsNull, result.Message);
        }

        [TestMethod]
        public void IsIncomingSigningDataValid_should_return_an_invalid_result_location_state_is_missing()
        {
            // Arrange
            _receiveSigningData.LocationState = string.Empty;

            // Act
            var result = _validIncomingSigningUtility.IsIncomingSigningDataValid(_receiveSigningData);

            // Assert
            Assert.IsFalse(result.Valid);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Message));
            Assert.AreEqual(ValidationMessages.LocationStateIsNull, result.Message);
        }

        [TestMethod]
        public void IsIncomingSigningDataValid_should_return_an_invalid_result_location_county_is_missing()
        {
            // Arrange
            _receiveSigningData.LocationCounty = string.Empty;

            // Act
            var result = _validIncomingSigningUtility.IsIncomingSigningDataValid(_receiveSigningData);

            // Assert
            Assert.IsFalse(result.Valid);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Message));
            Assert.AreEqual(ValidationMessages.LocationCountyIsNull, result.Message);
        }
    }
}
