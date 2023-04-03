using Cysharp.Threading.Tasks;

public class RocketBoostAction : IBoostAction
{
    private Board _board;
    private GoalCellOnBoardFinder _goalItemFinder;
    private ItemNextStateMover _itemRemover;
    private BoostExicuter _boostExicuter;
    private BoardClearer _boardClearer;
    private NeighbourRemover _neighbourRemover;

    private ICell _executedOnCell;

    public RocketBoostAction(Board board,
            GoalCellOnBoardFinder goalItemFinder,
            ItemNextStateMover itemRemover, BoostExicuter boostExicuter)
    {
        _goalItemFinder = goalItemFinder;
        _itemRemover = itemRemover;
        _boostExicuter = boostExicuter;
        _board = board;
        _boardClearer = new(board);
        _neighbourRemover = new(board, Remove);
    }

    public async UniTask Execute(IItem item)
    {
        _executedOnCell = _board[item.View.GetWorldPosition()];

        await UniTask.WhenAll( _itemRemover.MoveNextStateItemIn(_executedOnCell, 0),
           _neighbourRemover.RemoveNeighbours(_executedOnCell.GridPosition));

        _boardClearer.ClearCell(_executedOnCell);

        if (_goalItemFinder.TryFind(out var cell))
        {
            await Remove(cell);
        }
    }  

    private async UniTask Remove(ICell cell)
    {
        float removeTime = 0.04f;

        if (cell.Item is IBoostItem boost)
        {
            await ExequteAnotherBoost(boost);
        }

        await _itemRemover.MoveNextStateItemIn(cell, removeTime);

    }

    private async UniTask ExequteAnotherBoost(IBoostItem boost)
    {
        await _boostExicuter.Execute(boost);
    }
}