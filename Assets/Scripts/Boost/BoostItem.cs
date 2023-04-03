using UnityEngine.Events;

public class BoostItem : Item, IBoostItem
{
    private BoostTypes _type;

    public IItem SwapWith { get; set; }

    public event UnityAction<BoostTypes> SetedType;

    public event UnityAction<BoostTypes> LeavedBoostState;

    public void SetBoostType(BoostTypes value)
    {
        _type = value;
        SetedType?.Invoke(value);
    }
    public BoostTypes GetBoostType() => _type;

    protected override void OnStateChanged(ItemStateTypes old, ItemStateTypes @new)
    {
        base.OnStateChanged(old, @new);

        if (old == ItemStateTypes.Boost)
        {
            LeavedBoostState?.Invoke(_type);
        }
    }
}
