using UnityEngine;

[System.Serializable]
public class BackgroundConfig
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int StartLevel { get; private set; }
    [field: SerializeField] public int EndLevel { get; private set; }

}
