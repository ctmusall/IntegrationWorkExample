using OrderPlacement.Data;
using OrderPlacement.Factories;
using OrderPlacement.Models;

namespace OrderPlacement.Repositories
{
    public class ReswareOrderRepository : IReswareOrderRepository
    {
        private readonly ReswareOrderContext _reswareOrderContext;

        public ReswareOrderRepository() : this(OrderDependencyFactory.Resolve<ReswareOrderContext>())
        {
            
        }

        public ReswareOrderRepository(ReswareOrderContext reswareOrderContext)
        {
            _reswareOrderContext = reswareOrderContext;
        }

        public int SaveReaderResult(ReaderResult readerResult)
        {
            if (readerResult?.Order == null || readerResult.PropertyAddress == null || readerResult.BuyerSellersReaderResult?.BuyerSellers == null || readerResult.BuyerSellersReaderResult.BuyerSellerAddresses == null) return -1;

            _reswareOrderContext.Orders.Add(readerResult.Order);

            _reswareOrderContext.PropertyAddresses.Add(readerResult.PropertyAddress);

            _reswareOrderContext.BuyerSellers.AddRange(readerResult.BuyerSellersReaderResult.BuyerSellers);

            _reswareOrderContext.BuyerSellerAddresses.AddRange(readerResult.BuyerSellersReaderResult.BuyerSellerAddresses);

            return _reswareOrderContext.SaveChanges();
        }
    }
}