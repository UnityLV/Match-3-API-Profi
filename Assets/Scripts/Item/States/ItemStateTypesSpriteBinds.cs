using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ItemStateTypesSpriteBinds), menuName = "ScriptableObjects/" + nameof(ItemStateTypesSpriteBinds), order = 1)]
public class ItemStateTypesSpriteBinds : ScriptableObject
{
    [field: SerializeField] public ItemStateTypesSpriteBind[] Binds { get; private set; }
}

[System.Serializable]
public class ItemStateTypesSpriteBind
{
    [field: SerializeField] public ItemStateTypes State { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}
