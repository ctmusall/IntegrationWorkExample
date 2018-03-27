using System;
using System.Collections.Generic;
using OrderPlacement.Factory;
using OrderPlacement.Models;
using OrderPlacement.Parsers;
using Resware.Data.Order.Repository;

namespace OrderPlacement.Managers
{
    public class OrderResultManager : IOrderResultManager
    {
        private readonly OrderRepository _reswareOrderRepository;
        private readonly IOrderResultParser _orderResultParser;
        private readonly IOrderParser _orderParser;

        public OrderResultManager() : this(DependencyFactory.Resolve<OrderRepository>(), DependencyFactory.Resolve<IOrderResultParser>(), DependencyFactory.Resolve<IOrderParser>()) { }

        public OrderResultManager(OrderRepository reswareOrderRepository, IOrderResultParser orderResultParser, IOrderParser orderParser)
        {
            _reswareOrderRepository = reswareOrderRepository;
            _orderResultParser = orderResultParser;
            _orderParser = orderParser;
        }

        public ICollection<OrderResult> GetAllOrders()
        {
            try
            {
                var orders = _reswareOrderRepository.GetAllOrders();
                return _orderResultParser.ParseAllOrderResults(orders);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int UpdateOrder(OrderResult orderResult)
        {
            try
            {
                var order = _orderParser.ParseOrder(orderResult);
                return _reswareOrderRepository.UpdateOrder(order);
            }
            catch (Exception)
            {
                return -1;
            }
        }

    }
}