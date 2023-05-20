
using Cysharp.Threading.Tasks;

public class HorizontalLineRemover : BoostLineRemoverBase
{
    private IBoostExicuter _boostExicuter;

    private BoostTypes _boostType = BoostTypes.Horizontal;

    public HorizontalLineRemover(Board board, IBoostExicuter boostExicuter) : base(board)
    {
        Directons = new GridPosition[] { GridPosition.Left,GridPosition.Right};
        _boostExicuter = boostExicuter;
    }

    protected override async UniTask ExequteAnotherBoost(IBoostItem boost)
    {
        if (boost.GetBoostType() == _boostType)
        {
            boost.Init(BoostTypes.Vertical);
        }
        await _boostExicuter.Execute(boost);
    }
}
