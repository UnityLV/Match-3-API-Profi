using UnityEngine;

[CreateAssetMenu(fileName = nameof(Box2ObsticleItemMapFactory), menuName = "ScriptableObjects/" + nameof(Box2ObsticleItemMapFactory), order = 1)]

public class Box2ObsticleItemMapFactory : BaseObsticleItemMapFactory
{
    public override void Init(IStateObserver itemStateObserver)
    {
        base.Init(itemStateObserver);
        FactoryType = ItemStateTypes.Box2;
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
        item.Deactivate();
        return null;
    }
}
