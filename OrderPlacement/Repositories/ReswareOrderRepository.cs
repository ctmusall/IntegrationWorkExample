using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public List<Order> GetAllOrders()
        {
            return _reswareOrderContext.Orders
                .Include(o => o.PropertyAddress)
                .Include(o => o.BuyerAndSellers)
                .Include(o => o.BuyerAndSellers.Select(bs => bs.Address)).ToList();
        }

        public Order GetOrderById(Guid id)
        {
            return _reswareOrderContext.Orders
                .Include(o => o.PropertyAddress)
                .Include(o => o.BuyerAndSellers)
                .Include(o => o.BuyerAndSellers.Select(bs => bs.Address))
                .FirstOrDefault(o => o.Id == id);
        }

        public int DeleteOrderById(Guid id)
        {
            var order = _reswareOrderContext.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null) return 0;

            _reswareOrderContext.Orders.Remove(order);

            return _reswareOrderContext.SaveChanges();
        }

        public int UpdateOrder(Order updatedOrder)
        {
            var order = _reswareOrderContext.Orders.FirstOrDefault(o => o.Id == updatedOrder.Id);

            if (order == null) return 0;

            _reswareOrderContext.Entry(order).CurrentValues.SetValues(updatedOrder);

            return _reswareOrderContext.SaveChanges();
        }
    }
}