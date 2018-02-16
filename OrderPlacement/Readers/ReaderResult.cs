using System.Collections.Generic;

namespace OrderPlacement.Models
{
    public class ReaderResult
    {
        internal Order Order { get; set; }
        internal PropertyAddress PropertyAddress { get; set; }
        internal ICollection<BuyerSeller> BuyerSellers { get; set; }
        internal ICollection<BuyerSellerAddress> BuyerSellerAddresses { get; set; }
    }
}