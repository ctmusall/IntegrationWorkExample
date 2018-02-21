﻿using OrderPlacement.Data;
using OrderPlacement.Managers;
using OrderPlacement.Parser;
using OrderPlacement.Repositories;
using OrderPlacement.Utilities;
using Unity;

namespace OrderPlacement.Factories
{
    /// <summary>
    /// Simple wrapper for unity resolution.
    /// </summary>
    public class OrderDependencyFactory
    {
        /// <summary>
        /// Public reference to the unity container which will 
        /// allow the ability to register instrances or take 
        /// other actions on the container.
        /// </summary>
        public static IUnityContainer Container { get; }

        /// <summary>
        /// Static constructor for DependencyFactory which will 
        /// initialize the unity container.
        /// </summary>
        static OrderDependencyFactory()
        {
            var container = new UnityContainer();

            RegisterTypes(container);

            Container = container;
        }

        /// <summary>
        /// Resolves the type parameter T to an instance of the appropriate type.
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        public static T Resolve<T>()
        {
            T ret = default(T);

            if (Container.IsRegistered(typeof(T)))
            {
                ret = Container.Resolve<T>();
            }

            return ret;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ReswareReaderFactory>();
            container.RegisterType<ReswareOrderContext>();
            container.RegisterType<BuyerSellerReaderResultUtility>();
            container.RegisterType<IOrderPlacementManager, OrderPlacementManager>();
            container.RegisterType<IReswareOrderRepository, ReswareOrderRepository>();
            container.RegisterType<IOrderResultManager, OrderResultManager>();
            container.RegisterType<IOrderParser, OrderParser>();
        }
    }
}