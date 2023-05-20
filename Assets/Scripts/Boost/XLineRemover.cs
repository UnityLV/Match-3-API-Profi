using Cysharp.Threading.Tasks;

public class XLineRemover : BoostLineRemoverBase
{
    private IBoostExicuter _boostExicuter;

    public XLineRemover(Board board,  IBoostExicuter boostExicuter) : base(board)
    {
        Directons = new GridPosition[] { GridPosition.Left, GridPosition.Right, GridPosition.Up, GridPosition.Down };
        _boostExicuter = boostExicuter;
    }

    protected override async UniTask ExequteAnotherBoost(IBoostItem boost)
    {
        await _boostExicuter.Execute(boost);
    }
}


