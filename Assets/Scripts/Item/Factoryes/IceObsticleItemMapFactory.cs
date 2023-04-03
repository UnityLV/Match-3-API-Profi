using UnityEngine;

[CreateAssetMenu(fileName = nameof(IceObsticleItemMapFactory), menuName = "ScriptableObjects/" + nameof(IceObsticleItemMapFactory), order = 1)]

public class IceObsticleItemMapFactory : BaseObsticleItemMapFactory
{
    public override void Init(IStateObserver stateObserver)
    {
        base.Init(stateObserver);
        FactoryType = ItemStateTypes.Ice;
    }

    public override IItem Get(int column)
    {
        var item = GetItemFromPool();

        item.View.Hide();

        if (TryGetFromConfigPool(column, out var id))
        {
            bool isIdFound = id != Default;
            if (isIdFound)
            {
                item.SetState(FactoryType);
                item.SetId(id);
                item.View.SetSprite(id);
                item.View.SetSprite(item.State.Type);
                item.SetStateObserver(ItemStateObserver);

                return item;
            }

        }
        item.Deactivate();
        return null;
    }
}
