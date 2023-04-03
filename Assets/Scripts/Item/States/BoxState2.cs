using Cysharp.Threading.Tasks;

public class BoxState2 : ItemState
{
    private IItemView _itemView;

    public BoxState2(IItemView itemView)
    {
        _itemView = itemView;
    }

    public override async UniTask Entrance(float transitionTime)
    {
        Type = ItemStateTypes.Box2;
        _itemView.SetSprite(Type);
        IsCanBeOriginOfSequence = false;
        Movable = false;
        HasGravity = false;
    }

    public override async UniTask Exit(float transitionTime)
    {
       
    }
}
