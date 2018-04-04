using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderPlacement.Readers;
using OrderPlacement.Readers.Solidifi;
using OrderPlacement.Utilities;

namespace Resware.Orders.WCF.Test.Readers.Solidifi.Test
{
    [TestClass]
    public class SolidifiReswareReaderTest
    {
        private IReswareReader _reswareReader;
        private BuyerSellerReaderResultUtility _buyerSellerReaderResultUtility;

        [TestInitialize]
        public void Setup()
        {
            _buyerSellerReaderResultUtility = new BuyerSellerReaderResultUtility();
            _reswareReader = new SolidifiReswareReader(_buyerSellerReaderResultUtility);
        }

        [TestMethod]
        public void ParseInput_passed_valid_data_should_create_reader_result_with_order_property_address_and_buyer_seller_result()
        {
            // Act
            var result = _reswareReader.ParseInput("123456", new OrderPlacementServicePropertyAddress(), 11, DateTime.Now, new OrderPlacementServicePartner(), new []{new OrderPlacementServiceBuyerSeller()}, new []{new OrderPlacementServiceBuyerSeller()}, "Notes!", 1, 2);

            // Assert
            Assert.IsNotNull(result.Order);
            Assert.IsNotNull(result.PropertyAddress);
            Assert.IsNotNull(result.BuyerSellersReaderResult);
        }
    }
}
