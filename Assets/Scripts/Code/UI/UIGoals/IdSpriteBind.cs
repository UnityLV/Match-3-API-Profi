using UnityEngine;

[System.Serializable]
public class IdSpriteBind
{
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}

[System.Serializable]
public class SequenceSpriteBind
{
    [field: SerializeField] public SequenceTypes Type { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}
