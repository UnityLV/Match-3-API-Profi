using Cysharp.Threading.Tasks;

public abstract class BoostLineRemoverBase : IBoostAction
{
    protected GridPosition[] Directons;

    private Board _board;
    private ItemNextStateMover _itemNextStateMover;
    private BoardClearer _boardClearer;

    protected BoostLineRemoverBase(Board board)
    {
        _board = board;
        _itemNextStateMover = new();
        _boardClearer = new(board);
    }

    public async UniTask Execute(IItem item)
    {
        var cell = _board[item.View.GetWorldPosition()];

        await _itemNextStateMover.MoveNextStateItemIn(cell, 0);
        _boardClearer.ClearCell(cell);

        await UniTask.WhenAll(Directons.Select(x => RemoveLine(cell.GridPosition, x)));

    }

    private async UniTask RemoveLine(GridPosition position, GridPosition direction)
    {
        var positonOnLine = position += direction;

        bool isOnBoard = _board.IsPositionOnBoard(positonOnLine);

        while (isOnBoard)
        {
            if (IsCellItemBlokedLine(positonOnLine, out ICell cell))
            {
                await Remove(cell);
                return;
            }

            await Remove(cell);

            positonOnLine = position += direction;
            isOnBoard = _board.IsPositionOnBoard(positonOnLine);
        }

    }

    private bool IsCellItemBlokedLine(GridPosition positonOnLine, out ICell cell)
    {
        cell = _board[positonOnLine];

        if (cell.HasItem)
        {
            if (cell.Item.State.Type == ItemStateTypes.MovableBox1 ||
                cell.Item.State.Type == ItemStateTypes.MovableBox2)
            {
                return true;
            }
        }

        return false;
    }

    private async UniTask Remove(ICell cell)
    {
        float removeTime = 0.04f;

        if (cell.Item is IBoostItem boost)
        {
            await ExequteAnotherBoost(boost);
        }

        await _itemNextStateMover.MoveNextStateItemIn(cell, removeTime);

    }

    protected abstract UniTask ExequteAnotherBoost(IBoostItem boost);
}


