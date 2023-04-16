using Autofac;

namespace UserList
{
    public static class Resolver
    {
        private static IContainer container;

        public static void Initialize(IContainer container)
        {
            Resolver.container = container;
        }

        public static T Resolve<T>() => container.Resolve<T>();
    }
}
