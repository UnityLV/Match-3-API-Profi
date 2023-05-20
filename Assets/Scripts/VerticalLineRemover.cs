using Cysharp.Threading.Tasks;

public class VerticalLineRemover : BoostLineRemoverBase
{
    private IBoostExicuter _boostExicuter;

    private BoostTypes _boostType = BoostTypes.Vertical;

    public VerticalLineRemover(Board board, IBoostExicuter boostExicuter) : base(board)
    {
        Directons = new GridPosition[] { GridPosition.Up, GridPosition.Down };
        _boostExicuter = boostExicuter;
    }

    protected override async UniTask ExequteAnotherBoost(IBoostItem boost)
    {
        if (boost.GetBoostType() == _boostType)
        {
            boost.Init(BoostTypes.Horizontal);
        }
        await _boostExicuter.Execute(boost);
    }
}
