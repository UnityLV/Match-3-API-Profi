using Cysharp.Threading.Tasks;

public class DeadState : ItemState
{
    private IItemView _itemView;
    private IPooleable _pooleable;

    public DeadState(IItemView itemView,IPooleable pooleable)
    {
        _itemView = itemView;
        _pooleable = pooleable;
    }

    public override async UniTask Entrance(float transitionTime)
    {
        Type = ItemStateTypes.Dead;
        IsAlive = false;
        await _itemView.Hide(transitionTime);
        _pooleable.Deactivate();
    }

    public override async UniTask Exit(float transitionTime)
    {
        
    }
}
