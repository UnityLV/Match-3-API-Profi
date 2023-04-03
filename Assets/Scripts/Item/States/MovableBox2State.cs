using Cysharp.Threading.Tasks;

public class MovableBox2State : ItemState
{
    private IItemView _itemView;

    public MovableBox2State(IItemView itemView)
    {
        _itemView = itemView;
    }

    public override async UniTask Entrance(float transitionTime)
    {
        Type = ItemStateTypes.MovableBox2;
        _itemView.SetSprite(Type);
        IsCanBeOriginOfSequence = false;
        Movable = true;
        HasGravity = true;
    }

    public override async UniTask Exit(float transitionTime)
    {

    }
}
