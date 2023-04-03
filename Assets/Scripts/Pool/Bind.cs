using UnityEngine;

[System.Serializable]
public class Bind<T>
{
    [field: SerializeField] public T Type { get; private set; }
    [field: SerializeField] public Color32 Color { get; private set; }

}
