using Resware.Data.ActionEvent.Repository;
using Resware.Data.NoteDoc.Repository;
using Resware.Data.Order.Repository;
using Resware.Data.Signing.Repository;
using ReswareOrderMonitorService.Factories.ActionEvents;
using ReswareOrderMonitorService.Factories.CompletedActionEvents;
using ReswareOrderMonitorService.Factories.Documents;
using ReswareOrderMonitorService.Mirth;
using ReswareOrderMonitorService.Monitors;
using ReswareOrderMonitorService.Readers;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.Utilities;
using Unity;

namespace ReswareOrderMonitorService.Factories
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
            container.RegisterSingleton<OrderRepository>();
            container.RegisterSingleton<ActionEventRepository>();
            container.RegisterSingleton<SigningRepository>();
            container.RegisterSingleton<NoteDocRepository>();
            container.RegisterType<IParentActionEventFactory, ParentActionEventFactory>();
            container.RegisterType<IActionEventReader, ActionEventReader>();
            container.RegisterType<IParentClientCompletedActionEventFactory, ParentClientCompletedActionEventFactory>();
            container.RegisterType<IClientDocumentFactory, ClientDocumentFactory>();
            container.RegisterType<IIntegrationServiceRepository, IntegrationServiceRepository>();
            container.RegisterType<IMirthServiceClient, MirthServiceClient>();
            container.RegisterType<IDateTimeUtility, DateTimeUtility>();
            container.RegisterType<IParentServiceUtilityFactory, ParentServiceUtilityFactory>();
            container.RegisterType<IOrderActionEventMonitor, OrderActionEventMonitor>();
            container.RegisterType<IDocumentMonitor, DocumentMonitor>();
            container.RegisterType<IOutgoingMonitor, OutgoingMonitor>();
        }
    }
}