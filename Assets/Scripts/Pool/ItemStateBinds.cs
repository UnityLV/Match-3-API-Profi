using UnityEngine;

[CreateAssetMenu(fileName = nameof(ItemStateBinds), menuName = "ScriptableObjects/" + nameof(ItemStateBinds), order = 1)]
public class ItemStateBinds : ScriptableObject
{
    [SerializeField] private ItemStateTypes _default;

    [field: SerializeField] public Bind<ItemStateTypes>[] Value { get; private set; }

    public ItemStateTypes Default => _default;


    public static implicit operator Binds<ItemStateTypes>(ItemStateBinds itemStateBinds)
    {
        Binds<ItemStateTypes> bindsInt = new Binds<ItemStateTypes>();
        bindsInt.Value = itemStateBinds.Value;
        bindsInt.Default = itemStateBinds.Default;
        return bindsInt;
    }
    
}


