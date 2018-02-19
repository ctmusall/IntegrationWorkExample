using System.Collections.Generic;
using OrderPlacement.Models;

namespace OrderPlacement.Readers
{
    internal class BuyerSellerReaderResult
    {
        internal ICollection<BuyerSeller> BuyerSellers { get; set; }
        internal ICollection<BuyerSellerAddress> BuyerSellerAddresses { get; set; }
    }
}