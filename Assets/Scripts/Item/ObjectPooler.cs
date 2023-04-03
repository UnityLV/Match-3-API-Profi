using UnityEngine;
using System.Collections.Generic;
using Common.Extensions;

public class ObjectPooler<T> where T : MonoBehaviour , IPooleable
{
    private T _template;    
    private readonly Stack<IPooleable> _pool = new();

    public ObjectPooler(T template)
    {
        _template = template;
    }

    public T Get()
    {
        if (_pool.TryPop(out IPooleable pooleable))
        {
            pooleable.Deactivation += OnDeactivation;
            return pooleable as T;
        }

        IPooleable poolable = _template.gameObject.CreateNew<IPooleable>();
        poolable.Deactivation += OnDeactivation;

        return poolable as T;
    }

    private void OnDeactivation(IPooleable pooleable)
    {
        pooleable.Deactivation -= OnDeactivation;

        _pool.Push(pooleable);
    }

}
