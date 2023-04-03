using Cysharp.Threading.Tasks;

public class VerticalLineRemover : BoostLineRemoverBase
{
    private BoostExicuter _boostExicuter;

    private BoostTypes _boostType = BoostTypes.Vertical;

    public VerticalLineRemover(Board board, ItemNextStateMover itemRemover, BoostExicuter boostExicuter) : base(board, itemRemover)
    {
        Directons = new GridPosition[] { GridPosition.Up, GridPosition.Down };
        _boostExicuter = boostExicuter;
    }

    protected override async UniTask ExequteAnotherBoost(IBoostItem boost)
    {
        if (boost.GetBoostType() == _boostType)
        {
            boost.SetBoostType(BoostTypes.Horizontal);
        }
        await _boostExicuter.Execute(boost);
    }
}
