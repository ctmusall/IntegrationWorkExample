﻿using ActionEventService.Managers;
using ActionEventService.Readers;
using Resware.Data.ActionEvent.Repository;
using Unity;

namespace ActionEventService.Factories
{
    /// <summary>
    /// Simple wrapper for unity resolution.
    /// </summary>
    internal class DependencyFactory
    {
        /// <summary>
        /// Public reference to the unity container which will 
        /// allow the ability to register instrances or take 
        /// other actions on the container.
        /// </summary>
        internal static IUnityContainer Container { get; }

        /// <summary>
        /// Static constructor for DependencyFactory which will 
        /// initialize the unity container.
        /// </summary>
        static DependencyFactory()
        {
            var container = new UnityContainer();

            RegisterTypes(container);

            Container = container;
        }

        /// <summary>
        /// Resolves the type parameter T to an instance of the appropriate type.
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        internal static T Resolve<T>()
        {
            var ret = default(T);

            if (Container.IsRegistered(typeof(T)))
            {
                ret = Container.Resolve<T>();
            }

            return ret;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterSingleton<ActionEventReader>();
            container.RegisterSingleton<ActionEventRepository>();
            container.RegisterType<IActionEventManager, ActionEventManager>();
        }
    }
}