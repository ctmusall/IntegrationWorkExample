using ReswareOrderMonitorService.Mirth;
using ReswareOrderMonitorService.Monitors;
using ReswareOrderMonitorService.Readers;
using ReswareOrderMonitorService.Utilities;
using Unity;

namespace ReswareOrderMonitorService.Factories
{
    /// <summary>
    /// Simple wrapper for unity resolution.
    /// </summary>
    internal static class ReswareOrderDependencyFactory
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
        static ReswareOrderDependencyFactory()
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
            T ret = default(T);

            if (Container.IsRegistered(typeof(T)))
            {
                ret = Container.Resolve<T>();
            }

            return ret;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IOrderActionEventMonitor, IncomingOrderActionEventMonitor>();
            container.RegisterType<IActionEventReader, ActionEventReader>();
            container.RegisterType<IMirthServiceClient, MirthServiceClient>();
            container.RegisterType<IParentActionEventFactory, ParentActionEventFactory>();
            container.RegisterType<IParentServiceUtilityFactory, ParentServiceUtilityFactory>();
            container.RegisterType<IDateTimeUtility, DateTimeUtility>();
        }
    }
}