using SigningService.Data;
using SigningService.Managers;
using SigningService.Parsers;
using SigningService.Readers;
using SigningService.Repositories;
using SigningService.Utilities;
using Unity;

namespace SigningService.Factories
{
    /// <summary>
    /// Simple wrapper for unity resolution.
    /// </summary>
    public class SigningDependencyFactory
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
        static SigningDependencyFactory()
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
            container.RegisterType<ISigningManager, SigningManager>();
            container.RegisterType<SigningReader>();
            container.RegisterType<IReswareSigningRepository, ReswareSigningRepository>();
            container.RegisterType<ReswareSigningContext>();
            container.RegisterType<ISigningServiceResultManager, SigningServiceResultManager>();
            container.RegisterType<SigningResultParser>();
            container.RegisterType<SigningParser>();
            container.RegisterType<ValidIncomingSigningUtility>();
        }
    }
}