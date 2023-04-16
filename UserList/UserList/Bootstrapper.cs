using Autofac;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using UserList.Views;
using UserList.Repositories;
using UserList.ViewModels;

namespace UserList
{
    public abstract class Bootstrapper
    {

        protected ContainerBuilder ContainerBuilder
        { get; private set; }

        protected Bootstrapper()
        {
            Initialize();
            FinishInitialization();
        }

        protected virtual void Initialize()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            ContainerBuilder = new ContainerBuilder();

            var assemblyList =
                currentAssembly.DefinedTypes
                .Where(e => e.IsSubclassOf(typeof(Page)) ||
                             e.IsSubclassOf(typeof(ViewModel)) );

            foreach (var type in assemblyList)
            {
                ContainerBuilder.RegisterType( type.AsType() );
            }

            ContainerBuilder.RegisterType<UserlistItemRepository>()
                .SingleInstance();
        }

        private void FinishInitialization()
        {
            var container = ContainerBuilder.Build();
            Resolver.Initialize(container);
        }
    }
}
