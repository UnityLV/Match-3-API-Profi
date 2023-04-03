using UnityEngine;


[System.Serializable]
public class IntBind
{
    [field: SerializeField] public int Type { get; private set; }
    [field: SerializeField] public Color32 Color { get; private set; }
}
