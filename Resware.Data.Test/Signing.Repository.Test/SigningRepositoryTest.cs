using System.Collections.Generic;
using System.Linq;
using Effort;
using Effort.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Data.Context;
using Resware.Data.Signing.Repository;
using Resware.Entities.Signings.SigningParties;

namespace Resware.Data.Test.Signing.Repository.Test
{
    [TestClass]
    public class SigningRepositoryTest
    {
        private SigningRepository _signingRepository;
        private ReswareDbContext _reswareDbContext;

        [TestInitialize]
        public void Setup()
        {
            EffortProviderConfiguration.RegisterProvider();
            var connection = DbConnectionFactory.CreateTransient();
            _reswareDbContext = new ReswareDbContext(connection);
            _signingRepository = new SigningRepository(_reswareDbContext);
        }

        [TestMethod]
        public void SaveNewSigning_signing_is_null_return_negative_one()
        {
            // Act
            var result = _signingRepository.SaveNewSigning(null, new List<SigningParty>());

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void SaveNewSigning_passed_valid_signing_with_no_signing_parties_should_return_one()
        {
            // Arrange
            var signing = new Entities.Signings.Signing();

            // Act
            var result = _signingRepository.SaveNewSigning(signing, null);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void SaveNewSigning_passed_valid_signing_and_signing_parties_should_return_three()
        {
            // Arrange
            var signing = new Entities.Signings.Signing();

            var signingParties = new List<SigningParty> { new SigningParty(), new SigningParty() };
            
            // Act
            var result = _signingRepository.SaveNewSigning(signing, signingParties);
            
            // Assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void GetAllSignings_should_return_empty_list_of_signings()
        {
            // Act
            var result = _signingRepository.GetAllSignings();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetAllSignings_should_return_non_empty_list_signings_and_include_signing_parties()
        {
            // Arrange
            _reswareDbContext.Signings.Add(new Entities.Signings.Signing { SigningParties = new List<SigningParty> { new SigningParty() } });
            _reswareDbContext.SaveChanges();

            // Act
            var result = _signingRepository.GetAllSignings();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].SigningParties.Count);
        }

        [TestMethod]
        public void UpdateSigning_passed_signing_object_but_siging_does_not_exist_return_zero()
        {
            // Act
            var result = _signingRepository.UpdateSigning(new Entities.Signings.Signing());

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void UpdateSigning_passed_in_signing_and_updated_signing_fields_should_return_one()
        {
            // Arrange
            _reswareDbContext.Signings.Add(new Entities.Signings.Signing { FileNumber = "123456", ClosingLocation = "PA" });
            _reswareDbContext.SaveChanges();

            var updatedSigning = _reswareDbContext.Signings.First();
            updatedSigning.ClosingLocation = "MA";

            // Act
            var result = _signingRepository.UpdateSigning(updatedSigning);

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}
