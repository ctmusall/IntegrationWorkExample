using ReceiveNote.Data;
using ReceiveNote.Managers;
using ReceiveNote.Readers;
using ReceiveNote.Repositories;
using Unity;

namespace ReceiveNote.Factories
{
    /// <summary>
    /// Simple wrapper for unity resolution.
    /// </summary>
    public class NoteDocDependencyFactory
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
        static NoteDocDependencyFactory()
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
            container.RegisterType<INoteDocManager, NoteDocManager>();
            container.RegisterType<NoteDocReader>();
            container.RegisterType<IReswareNoteDocRepository, ReswareNoteDocRepository>();
            container.RegisterType<ReswareNoteDocContext>();
        }
    }
}