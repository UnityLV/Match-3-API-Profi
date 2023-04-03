using Cysharp.Threading.Tasks;

public class XLineRemover : BoostLineRemoverBase
{
    private BoostExicuter _boostExicuter;

    public XLineRemover(Board board, ItemNextStateMover itemRemover, BoostExicuter boostExicuter) : base(board, itemRemover)
    {
        Directons = new GridPosition[] { GridPosition.Left, GridPosition.Right, GridPosition.Up, GridPosition.Down };
        _boostExicuter = boostExicuter;
    }

    protected override async UniTask ExequteAnotherBoost(IBoostItem boost)
    {
        await _boostExicuter.Execute(boost);
    }
}


