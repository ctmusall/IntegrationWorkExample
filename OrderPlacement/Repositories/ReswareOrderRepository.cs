using OrderPlacement.Data;
using OrderPlacement.Factories;
using OrderPlacement.Models;

namespace OrderPlacement.Repositories
{
    public class ReswareOrderRepository : IReswareOrderRepository
    {
        private readonly ReswareOrderContext _reswareOrderContext;

        public ReswareOrderRepository() : this(DependencyFactory.Resolve<ReswareOrderContext>())
        {
            
        }

        public ReswareOrderRepository(ReswareOrderContext reswareOrderContext)
        {
            _reswareOrderContext = reswareOrderContext;
        }

        public int SaveReaderResult(ReaderResult readerResult)
        {
            _reswareOrderContext.Orders.Add(readerResult.Order);

            _reswareOrderContext.PropertyAddresses.Add(readerResult.PropertyAddress);

            _reswareOrderContext.BuyerSellers.AddRange(readerResult.BuyerSellers);

            _reswareOrderContext.BuyerSellerAddresses.AddRange(readerResult.BuyerSellerAddresses);

            return _reswareOrderContext.SaveChanges();
        }
    }
}