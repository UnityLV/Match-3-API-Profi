using Cysharp.Threading.Tasks;

public class RemoverTool : SingleCellTool
{
    private float _itemExecuteSpeed = 0.3f;

    public RemoverTool(CellSelector cellSelector, BoardSolver boardSolver) :base(cellSelector, boardSolver)
    {
    }

    protected override async UniTask ToolAction(ICell cell)
    {
        await cell.Item.MoveNextState(_itemExecuteSpeed);
    }
}
