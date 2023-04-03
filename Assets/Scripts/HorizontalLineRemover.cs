
using Cysharp.Threading.Tasks;

public class HorizontalLineRemover : BoostLineRemoverBase
{
    private BoostExicuter _boostExicuter;

    private BoostTypes _boostType = BoostTypes.Horizontal;

    public HorizontalLineRemover(Board board, ItemNextStateMover itemRemover, BoostExicuter boostExicuter) : base(board, itemRemover)
    {
        Directons = new GridPosition[] { GridPosition.Left,GridPosition.Right};
        _boostExicuter = boostExicuter;
    }

    protected override async UniTask ExequteAnotherBoost(IBoostItem boost)
    {
        if (boost.GetBoostType() == _boostType)
        {
            boost.SetBoostType(BoostTypes.Vertical);
        }
        await _boostExicuter.Execute(boost);
    }
}
