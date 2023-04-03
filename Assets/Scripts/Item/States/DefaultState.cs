using Cysharp.Threading.Tasks;

public class DefaultState : ItemState
{
    private IItemView _itemView;

    public DefaultState(IItemView itemView)
    {
        _itemView = itemView;
    }

    public override async UniTask Entrance(float transitionTime)
    {
        Type = ItemStateTypes.Default;
        _itemView.SetSprite(Type);
        Movable = true;
        HasGravity = true;
    }

    public override async UniTask Exit(float transitionTime)
    {
        
    }

}
