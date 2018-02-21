using System;
using System.Collections.Generic;
using OrderPlacement.Factories;
using OrderPlacement.Models;
using OrderPlacement.Parser;
using OrderPlacement.Repositories;

namespace OrderPlacement.Managers
{
    public class OrderResultManager : IOrderResultManager
    {
        private readonly IReswareOrderRepository _reswareOrderRepository;
        private readonly IOrderParser _orderParser;

        public OrderResultManager() : this(OrderDependencyFactory.Resolve<IReswareOrderRepository>(), OrderDependencyFactory.Resolve<IOrderParser>()) { }

        public OrderResultManager(IReswareOrderRepository reswareOrderRepository, IOrderParser orderParser)
        {
            _reswareOrderRepository = reswareOrderRepository;
            _orderParser = orderParser;
        }

        public ICollection<OrderResult> GetAllOrders()
        {
            try
            {
                var orders = _reswareOrderRepository.GetAllOrders();
                return _orderParser.ParseOrderResults(orders);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}