using Cysharp.Threading.Tasks;

public class MovableBox1State : ItemState
{
    private IItemView _itemView;

    public MovableBox1State(IItemView itemView)
    {
        _itemView = itemView;
    }

    public override async UniTask Entrance(float transitionTime)
    {
        Type = ItemStateTypes.MovableBox1;
        _itemView.SetSprite(Type);
        IsCanBeOriginOfSequence = false;
        Movable = true;
        HasGravity = true;
    }

    public override async UniTask Exit(float transitionTime)
    {
        
    }
}



