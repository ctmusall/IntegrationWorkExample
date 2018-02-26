using System.Collections.Generic;
using System.Linq;
using OrderPlacement.Models;

namespace OrderPlacement.Parsers
{
    public class OrderParser : IOrderParser
    {
        public Order ParseOrder(OrderResult orderResult)
        {
            return new Order
            {
                Id = orderResult.Id,
                FileNumber = orderResult.FileNumber,
                CustomerId = orderResult.CustomerId,
                BuyerAndSellers = ParseBuyersAndSellers(orderResult.BuyersAndSellers),
                ClosingDateTime = orderResult.ClosingDateTime,
                CreatedDateTime = orderResult.CreatedDateTime,
                CustomerContact = orderResult.CustomerContact,
                DeliveryMethod = orderResult.DeliveryMethod,
                LenderName = orderResult.LenderName,
                Product = orderResult.Product,
                Notes = orderResult.Notes,
                PropertyAddress = ParsePropertyAddress(orderResult.PropertyAddress)
            };
        }
        private static ICollection<BuyerSeller> ParseBuyersAndSellers(IEnumerable<BuyerSellerResult> buyersAndSellers)
        {
            return buyersAndSellers.Select(buyerAndSeller => new BuyerSeller
            {
                Id = buyerAndSeller.Id,
                OrderId = buyerAndSeller.OrderId,
                Prefix = buyerAndSeller.Prefix,
                FirstName = buyerAndSeller.FirstName,
                MiddleName = buyerAndSeller.MiddleName,
                LastName = buyerAndSeller.LastName,
                Suffix = buyerAndSeller.Suffix,
                EntityName = buyerAndSeller.EntityName,
                MaritalStatus = buyerAndSeller.MaritalStatus,
                Phone = buyerAndSeller.Phone,
                Email = buyerAndSeller.Email,
                Spouse = buyerAndSeller.Spouse,
                Type = buyerAndSeller.Type,
                Address = ParseBuyerAndSellerAddress(buyerAndSeller.Address)
            }).ToList();
        }
        private static ICollection<BuyerSellerAddress> ParseBuyerAndSellerAddress(IEnumerable<BuyerSellerAddressResult> buyerSellerAddresses)
        {
            return buyerSellerAddresses.Select(buyerSellerAddress => new BuyerSellerAddress
            {
                BuyerSellerId = buyerSellerAddress.BuyerSellerId,
                Id = buyerSellerAddress.Id,
                StreetNumber = buyerSellerAddress.StreetNumber,
                StreetDirection = buyerSellerAddress.StreetDirection,
                StreetName = buyerSellerAddress.StreetName,
                StreetSuffix = buyerSellerAddress.StreetSuffix,
                Unit = buyerSellerAddress.Unit,
                Zip = buyerSellerAddress.Zip,
                City = buyerSellerAddress.City,
                State = buyerSellerAddress.State,
                AddressStreetInfo = buyerSellerAddress.AddressStreetInfo,
                Description = buyerSellerAddress.Description
            }).ToList();
        }
        private static ICollection<PropertyAddress> ParsePropertyAddress(IEnumerable<PropertyAddressResult> propertyAddresses)
        {
            return propertyAddresses.Select(propertyAddress => new PropertyAddress
            {
                Id = propertyAddress.Id,
                StreetNumber = propertyAddress.StreetNumber,
                StreetDirection = propertyAddress.StreetDirection,
                StreetName = propertyAddress.StreetName,
                StreetSuffix = propertyAddress.StreetSuffix,
                Unit = propertyAddress.Unit,
                Zip = propertyAddress.Zip,
                City = propertyAddress.City,
                State = propertyAddress.State,
                AddressStreetInfo = propertyAddress.AddressStreetInfo,
                Description = propertyAddress.Description,
                County = propertyAddress.County
            }).ToList();
        }
    }
}