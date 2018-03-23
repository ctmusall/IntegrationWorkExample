using ActionEventService.Managers;
using ActionEventService.Parsers;
using ActionEventService.Repositories;
using ActionEventService.Utilities;
using Unity;

namespace ActionEventService.Factories
{
    /// <summary>
    /// Simple wrapper for unity resolution.
    /// </summary>
    public class ActionEventDependencyFactory
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
        static ActionEventDependencyFactory()
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
            container.RegisterType<IActionEventManager, ActionEventManager>();
            container.RegisterType<IReswareActionEventRepository, ReswareActionEventRepository>();
            container.RegisterType<ActionEventReader>();
            container.RegisterType<ActionEventParser>();
            container.RegisterType<ActionEventResultParser>();
            container.RegisterType<IActionEventServiceResultManager, ActionEventServiceResultManager>();
            container.RegisterType<ValidIncomingActionEventUtility>();
        }
    }
}