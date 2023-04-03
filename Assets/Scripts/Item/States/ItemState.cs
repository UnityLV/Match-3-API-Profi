
using Cysharp.Threading.Tasks;

public abstract class ItemState
{    
    public bool IsAlive { get; protected set; } = true;
    public bool HasGravity { get; protected set; } = true;
    public bool Movable { get; protected set; } = true;
    public bool IsCanBeOriginOfSequence { get; protected set; } = true;    

    public ItemStateTypes Type { get; protected set; } = ItemStateTypes.Default;

    public abstract UniTask Exit(float transitionTime = 0.1f);
    public abstract UniTask Entrance(float transitionTime = 0.1f);

}
