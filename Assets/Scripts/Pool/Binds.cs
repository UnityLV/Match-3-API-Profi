using UnityEngine;

public class Binds<T> : ScriptableObject
{
    [field: SerializeField] public Bind<T>[] Value { get; set; }
    [field: SerializeField] public T Default { get; set; }
}


