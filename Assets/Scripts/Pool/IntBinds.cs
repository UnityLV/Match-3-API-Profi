using UnityEngine;

[CreateAssetMenu(fileName = nameof(IntBinds), menuName = "ScriptableObjects/" + nameof(IntBinds), order = 1)]
public class IntBinds : ScriptableObject
{
    [SerializeField] private int _default = -1;

    [field: SerializeField] public Bind<int>[] Value { get; private set; }

    public int Default => _default;

    public static implicit operator Binds<int>(IntBinds intBinds)
    {
        Binds<int> bindsInt = new Binds<int>();
        bindsInt.Value = intBinds.Value;
        bindsInt.Default = intBinds.Default;
        return bindsInt;
    }
}




