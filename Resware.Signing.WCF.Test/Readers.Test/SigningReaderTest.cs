using Adeptive.ResWare.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigningService.Readers;

namespace Resware.Signing.WCF.Test.Readers.Test
{
    [TestClass]
    public class SigningReaderTest
    {
        private SigningReader _signingReader;
        private ReceiveSigningData _receiveSigningData;

        [TestInitialize]
        public void Setup()
        {
            _signingReader = new SigningReader();
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
        public void ParseInput_should_return_signing_reader_result_with_mapped_signing_data_and_no_signing_parties()
        {
            // Act
            var result = _signingReader.ParseInput(_receiveSigningData);

            // Assert
            Assert.IsNotNull(result.Signing);
            Assert.AreEqual(_receiveSigningData.FileNumber, result.Signing.FileNumber);
            Assert.IsNull(result.SigningParties);
        }

        [TestMethod]
        public void ParseInput_should_return_signing_reader_result_with_mapped_signing_data_and_two_signing_parties()
        {
            // Arrange
            _receiveSigningData.SigningParties = new[] {new ReceiveSigningParty {Name = "Billy Bob", Phone = "1234567890"}, new ReceiveSigningParty {Name = "Sally Sue", Phone = "22222222222"}};

            // Act
            var result = _signingReader.ParseInput(_receiveSigningData);

            // Assert
            Assert.IsNotNull(result.Signing);
            Assert.AreEqual(_receiveSigningData.FileNumber, result.Signing.FileNumber);
            Assert.IsNotNull(result.SigningParties);
            Assert.AreEqual(2, result.SigningParties.Count);
        }
    }
}
