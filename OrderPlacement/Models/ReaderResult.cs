﻿using Resware.Entities.Orders;
using Resware.Entities.Orders.Addresses;

namespace OrderPlacement.Models
{
    public class ReaderResult
    {
        internal Order Order { get; set; }
        internal PropertyAddress PropertyAddress { get; set; }
        internal BuyerSellerReaderResult BuyerSellersReaderResult { get; set; }
    }
}