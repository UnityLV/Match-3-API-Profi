using Cysharp.Threading.Tasks;
using System;

public class BoostState : ItemState
{
    private IItemView _itemView;
    private Func<int> _getId;    

    public BoostState(IItemView itemView, Func<int> getId)
    {
        _itemView = itemView;
        _getId = getId;
    }

    public override async UniTask Entrance(float transitionTime)
    {
        Type = ItemStateTypes.Boost;
        _itemView.SetSprite((BoostTypes)(_getId()));        
        IsCanBeOriginOfSequence = false;
        Movable = true;
        HasGravity = true;
    }

    public override async UniTask Exit(float transitionTime)
    {
        IsCanBeOriginOfSequence = true;
    }
}
