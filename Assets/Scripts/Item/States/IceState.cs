using Cysharp.Threading.Tasks;

public class IceState : ItemState
{
    private IItemView _itemView;
    private int _id;

    public IceState(IItemView itemView, int id)
    {
        _itemView = itemView;
        _id = id;
    }

    public override async UniTask Entrance(float transitionTime)
    {
        Type = ItemStateTypes.Ice;
        _itemView.SetSprite(Type);
        _itemView.SetSprite(_id);
        IsCanBeOriginOfSequence = false;
        Movable = false;
        HasGravity = false;
    }

    public override async UniTask Exit(float transitionTime)
    {
        Movable = true;
        HasGravity = true;
    }
}
