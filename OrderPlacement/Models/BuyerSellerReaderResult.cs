using System.Collections.Generic;
using Resware.Entities.Orders.Addresses;
using Resware.Entities.Orders.BuyerSellers;

namespace OrderPlacement.Models
{
    internal class BuyerSellerReaderResult
    {
        internal ICollection<BuyerSeller> BuyerSellers { get; set; }
        internal ICollection<BuyerSellerAddress> BuyerSellerAddresses { get; set; }
    }
}