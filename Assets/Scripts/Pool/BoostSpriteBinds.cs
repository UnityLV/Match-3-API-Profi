
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BoostSpriteBinds), menuName = "ScriptableObjects/" + nameof(BoostSpriteBinds), order = 1)]
public class BoostSpriteBinds : ScriptableObject
{
    [field: SerializeField] public BoostSpriteBind[] Binds { get; private set; }  


}

