using UnityEngine;

[CreateAssetMenu(fileName = nameof(MovableBox2ObsticleItemMapFactory), menuName = "ScriptableObjects/" + nameof(MovableBox2ObsticleItemMapFactory), order = 1)]

public class MovableBox2ObsticleItemMapFactory : BaseObsticleItemMapFactory
{
    public override void Init(IStateObserver stateObserver)
    {
        base.Init(stateObserver);
        FactoryType = ItemStateTypes.MovableBox2;
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
                item.SetId((int)FactoryType);
                item.SetState(FactoryType);
                item.View.SetSprite(item.State.Type);
                item.SetStateObserver(ItemStateObserver);

                return item;
            }

        }

        return null;
    }
}

