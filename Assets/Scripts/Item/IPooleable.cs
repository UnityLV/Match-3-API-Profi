using UnityEngine.Events;

public interface IPooleable
{
    event UnityAction<IPooleable> Deactivation;

    void Deactivate();
}
