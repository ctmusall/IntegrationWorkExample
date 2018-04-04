using OrderPlacement.Models;
using Resware.Entities.Orders;
using Resware.Entities.Orders.Addresses;
using Resware.Entities.Orders.BuyerSellers;
using ReswareCommon.Enums;

namespace OrderPlacement.Utilities
{
    internal class BuyerSellerReaderResultUtility
    {
        internal void CreateBuyerSellerAndBuyerSellerAddressAssociation(OrderPlacementServiceBuyerSeller buyerSeller, Order order, BuyerSellerEnum type, BuyerSellerReaderResult result)
        {
            var bs = new BuyerSeller
            {
                Order = order,
                Prefix = buyerSeller?.Prefix,
                FirstName = buyerSeller?.FirstName,
                LastName = buyerSeller?.LastName,
                Suffix = buyerSeller?.Suffix,
                EntityName = buyerSeller?.EntityName,
                MaritalStatus = buyerSeller?.MaritalStatus,
                Phone = buyerSeller?.Phone,
                Email = buyerSeller?.Email,
                Spouse = false,
                Type = type
            };
            result.BuyerSellers.Add(bs);

            result.BuyerSellerAddresses.Add(new BuyerSellerAddress
            {
                StreetNumber = buyerSeller?.Address?.StreetNumber,
                StreetDirection = buyerSeller?.Address?.StreetDirection,
                StreetName = buyerSeller?.Address?.StreetName,
                StreetSuffix = buyerSeller?.Address?.StreetSuffix,
                Unit = buyerSeller?.Address?.Unit,
                Zip = buyerSeller?.Address?.Zip,
                City = buyerSeller?.Address?.City,
                State = buyerSeller?.Address?.State,
                AddressStreetInfo = buyerSeller?.Address?.AddressStreetInfo,
                Description = buyerSeller?.Address?.Description,
                BuyerSeller = bs
            });

            if (buyerSeller?.Spouse == null) return;

            var bsSpouse = new BuyerSeller {
                Order = order,
                Prefix = buyerSeller.Spouse?.Prefix,
                FirstName = buyerSeller.Spouse?.FirstName,
                LastName = buyerSeller.Spouse?.LastName,
                Suffix = buyerSeller.Spouse?.Suffix,
                EntityName = buyerSeller.Spouse?.EntityName,
                MaritalStatus = buyerSeller.Spouse?.MaritalStatus,
                Phone = buyerSeller.Spouse?.Phone,
                Email = buyerSeller.Spouse?.Email,
                Spouse = true,
                Type = type
            };

            result.BuyerSellers.Add(bsSpouse);

            result.BuyerSellerAddresses.Add(new BuyerSellerAddress
            {
                StreetNumber = buyerSeller.Address?.StreetNumber,
                StreetDirection = buyerSeller.Address?.StreetDirection,
                StreetName = buyerSeller.Address?.StreetName,
                StreetSuffix = buyerSeller.Address?.StreetSuffix,
                Unit = buyerSeller.Address?.Unit,
                Zip = buyerSeller.Address?.Zip,
                City = buyerSeller.Address?.City,
                State = buyerSeller.Address?.State,
                AddressStreetInfo = buyerSeller.Address?.AddressStreetInfo,
                Description = buyerSeller.Address?.Description,
                BuyerSeller = bsSpouse
            });
        }
    }
}