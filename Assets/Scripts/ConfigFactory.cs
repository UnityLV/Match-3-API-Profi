using UnityEngine;

public abstract class ConfigFactory<I,T> : ScriptableObject
{
    public bool TryGet(I p, out T t) => (t = Get(p)) != null;

    public abstract T Get(I information);

    public abstract void Init(IStateObserver itemStateObserver);


}
