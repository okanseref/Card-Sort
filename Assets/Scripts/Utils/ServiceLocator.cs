public static class ServiceLocator
{
    private static readonly DiContainer _container = new();

    public static void Register<T>(T instance)
    {
        _container.Register(instance);
    }

    public static T Resolve<T>()
    {
        return _container.Resolve<T>();
    }

    public static void Clear()
    {
        _container.Clear();
    }
}