using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Resware.Data.Repository;
using Resware.Entities.Orders.Addresses;
using Resware.Entities.Orders.BuyerSellers;

namespace Resware.Data.Order.Repository
{
    public class OrderRepository : RepositoryBase
    {
        public int SaveNewOrder(Entities.Orders.Order order, PropertyAddress propertyAddress, ICollection<BuyerSeller> buyerSellers, ICollection<BuyerSellerAddress> buyerSellerAddresses)
        {
            if (order == null || propertyAddress == null || buyerSellers == null || buyerSellerAddresses == null) return -1;

            if (string.IsNullOrWhiteSpace(order.FileNumber)) return -1;

            var orderExists = GetOrderByClientIdAndFileNumber(order.ClientId, order.FileNumber);

            if (orderExists != null) ReswareDbContext.Orders.Remove(orderExists);

            ReswareDbContext.Orders.Add(order);

            ReswareDbContext.PropertyAddresses.Add(propertyAddress);

            ReswareDbContext.BuyerSellers.AddRange(buyerSellers);

            ReswareDbContext.BuyerSellerAddresses.AddRange(buyerSellerAddresses);

            return ReswareDbContext.SaveChanges();
        }

        public List<Entities.Orders.Order> GetAllOrders()
        {
            return ReswareDbContext.Orders
                .Include(o => o.PropertyAddress)
                .Include(o => o.BuyerAndSellers)
                .Include(o => o.BuyerAndSellers.Select(bs => bs.Address)).ToList();
        }

        public int UpdateOrder(Entities.Orders.Order updatedOrder)
        {
            var order = ReswareDbContext.Orders.FirstOrDefault(o => o.Id == updatedOrder.Id);

            if (order == null) return 0;

            ReswareDbContext.Entry(order).CurrentValues.SetValues(updatedOrder);

            return ReswareDbContext.SaveChanges();
        }

        private Entities.Orders.Order GetOrderByClientIdAndFileNumber(int clientId, string fileNumber)
        {
            return ReswareDbContext.Orders
                .Include(o => o.PropertyAddress)
                .Include(o => o.BuyerAndSellers)
                .Include(o => o.BuyerAndSellers.Select(a => a.Address))
                .FirstOrDefault(o => string.Equals(o.FileNumber, fileNumber)
                && clientId == o.ClientId);
        }
    }
}