using OrderPlacement.Common;
using OrderPlacement.Models;
using OrderPlacement.Readers;

namespace OrderPlacement.Utilities
{
    internal class BuyerSellerReaderResultUtility
    {
        internal void CreateBuyerSellerAndBuyerSellerAddressAssociation(OrderPlacementServiceBuyerSeller buyerSeller, Order order, BuyerSellerEnum type, BuyerSellerReaderResult result)
        {
            var bs = new BuyerSeller
            {
                Order = order,
                Prefix = buyerSeller.Prefix,
                FirstName = buyerSeller.FirstName,
                LastName = buyerSeller.LastName,
                Suffix = buyerSeller.Suffix,
                EntityName = buyerSeller.EntityName,
                MaritalStatus = buyerSeller.MaritalStatus,
                Phone = buyerSeller.Phone,
                Email = buyerSeller.Email,
                Spouse = string.Equals(buyerSeller.MaritalStatus, "Married") && buyerSeller.Spouse == null,
                Type = type
            };
            result.BuyerSellers.Add(bs);
            result.BuyerSellerAddresses.Add(new BuyerSellerAddress
            {
                StreetNumber = buyerSeller.Address.StreetNumber,
                StreetDirection = buyerSeller.Address.StreetDirection,
                StreetName = buyerSeller.Address.StreetName,
                StreetSuffix = buyerSeller.Address.StreetSuffix,
                Unit = buyerSeller.Address.Unit,
                Zip = buyerSeller.Address.Zip,
                City = buyerSeller.Address.City,
                State = buyerSeller.Address.State,
                AddressStreetInfo = buyerSeller.Address.AddressStreetInfo,
                Description = buyerSeller.Address.Description,
                BuyerSeller = bs
            });
        }
    }
}