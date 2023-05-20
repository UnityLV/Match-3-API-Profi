using Cysharp.Threading.Tasks;

public class XLinerTool : SingleCellTool
{

    private IBoostExicuter _boostExicuter;

    public XLinerTool(CellSelector cellSelector, BoardSolver boardSolver, IBoostExicuter boostExicuter) : base(cellSelector, boardSolver)
    {
        _boostExicuter = boostExicuter;
    }

    protected override async UniTask ToolAction(ICell cell)
    {
        await _boostExicuter.Execute(cell.Item, ToolTypes.XLiner);
    }

}