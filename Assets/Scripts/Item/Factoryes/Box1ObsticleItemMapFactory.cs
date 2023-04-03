using UnityEngine;

[CreateAssetMenu(fileName = nameof(Box1ObsticleItemMapFactory), menuName = "ScriptableObjects/" + nameof(Box1ObsticleItemMapFactory), order = 1)]

public class Box1ObsticleItemMapFactory : BaseObsticleItemMapFactory
{
    public override void Init(IStateObserver itemStateObserver)
    {
        base.Init(itemStateObserver);
        FactoryType = ItemStateTypes.Box1;
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
                item.View.SetSprite(item.State.Type);
                item.SetStateObserver(ItemStateObserver);
                return item;
            }

        }

        return null;
    }
}
