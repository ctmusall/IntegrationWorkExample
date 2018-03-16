using System.Collections.Generic;

namespace OrderPlacement.Models
{
    internal class BuyerSellerReaderResult
    {
        internal ICollection<BuyerSeller> BuyerSellers { get; set; }
        internal ICollection<BuyerSellerAddress> BuyerSellerAddresses { get; set; }
    }
}