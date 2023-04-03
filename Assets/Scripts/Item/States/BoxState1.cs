using Cysharp.Threading.Tasks;

public class BoxState1 : ItemState
{
    private IItemView _itemView;    

    public BoxState1(IItemView itemView)
    {
        _itemView = itemView;        
    }

    public override async UniTask Entrance(float transitionTime)
    {
        Type = ItemStateTypes.Box1;
        _itemView.SetSprite(Type);       
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
