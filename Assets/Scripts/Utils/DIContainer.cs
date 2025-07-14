using System;
using System.Collections.Generic;

public class DiContainer
{
    private Dictionary<Type, object> _services = new();

    public void Register<T>(T instance)
    {
        _services[typeof(T)] = instance;
    }

    public T Resolve<T>()
    {
        return (T)_services[typeof(T)];
    }

    public void Clear()
    {
        _services.Clear();
    }
}