using System;
using System.Collections.Generic;

public class SignalBus : Singleton<SignalBus>
{
    private readonly Dictionary<Type, Delegate> _eventTable = new Dictionary<Type, Delegate>();

    // To track wrapped parameterless delegates
    private readonly Dictionary<(Type, Delegate), Delegate> _wrappers = new Dictionary<(Type, Delegate), Delegate>();

    public void Subscribe<T>(Action<T> handler)
    {
        var type = typeof(T);
        if (_eventTable.TryGetValue(type, out var existing))
            _eventTable[type] = Delegate.Combine(existing, handler);
        else
            _eventTable[type] = handler;
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
        var type = typeof(T);
        if (_eventTable.TryGetValue(type, out var existing))
        {
            var current = Delegate.Remove(existing, handler);
            if (current == null)
                _eventTable.Remove(type);
            else
                _eventTable[type] = current;
        }
    }

    // Subscribe without payload
    public void Subscribe<T>(Action handler) where T : new()
    {
        void Wrapper(T signal) => handler();

        var type = typeof(T);
        var key = (type, handler);

        // Avoid duplicate subscription
        if (_wrappers.ContainsKey(key)) return;

        Action<T> wrapped = Wrapper;
        _wrappers[key] = wrapped;
        Subscribe(wrapped);
    }

    // Unsubscribe without payload
    public void Unsubscribe<T>(Action handler) where T : new()
    {
        var key = (typeof(T), handler);
        if (_wrappers.TryGetValue(key, out var wrapped))
        {
            Unsubscribe((Action<T>)wrapped);
            _wrappers.Remove(key);
        }
    }

    public void Fire<T>(T signal)
    {
        var type = typeof(T);
        if (_eventTable.TryGetValue(type, out var del))
            (del as Action<T>)?.Invoke(signal);
    }

    public void Fire<T>() where T : new()
    {
        Fire(new T());
    }
}