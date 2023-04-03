using UnityEngine;

[System.Serializable]
public class BoostSpriteBind
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public BoostTypes Type { get; private set; }

}