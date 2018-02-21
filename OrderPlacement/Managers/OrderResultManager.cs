using System;
using System.Collections.Generic;
using OrderPlacement.Factories;
using OrderPlacement.Models;
using OrderPlacement.Parser;
using OrderPlacement.Parsers;
using OrderPlacement.Repositories;

namespace OrderPlacement.Managers
{
    public class OrderResultManager : IOrderResultManager
    {
        private readonly IReswareOrderRepository _reswareOrderRepository;
        private readonly IOrderResultParser _orderResultParser;
        private readonly IOrderParser _orderParser;

        public OrderResultManager() : this(OrderDependencyFactory.Resolve<IReswareOrderRepository>(), OrderDependencyFactory.Resolve<IOrderResultParser>(), OrderDependencyFactory.Resolve<IOrderParser>()) { }

        public OrderResultManager(IReswareOrderRepository reswareOrderRepository, IOrderResultParser orderResultParser, IOrderParser orderParser)
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

        public OrderResult GetOrderById(Guid id)
        {
            try
            {
                var order = _reswareOrderRepository.GetOrderById(id);
                return _orderResultParser.ParseOrderResult(order);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int DeleteOrderById(Guid id)
        {
            try
            {
                return _reswareOrderRepository.DeleteOrderById(id);
            }
            catch (Exception)
            {
                return -1;
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