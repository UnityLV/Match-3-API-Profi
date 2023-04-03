using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

public class RainbowBoostAction : IBoostAction
{
    private Board _board;
    private ItemNextStateMover _itemRemover;
    private BoostExicuter _boostExicuter;
    private BoardClearer _boardClearer;

    public RainbowBoostAction(Board board, ItemNextStateMover itemRemover, BoostExicuter boostExicuter)
    {
        _board = board;
        _itemRemover = itemRemover;
        _boostExicuter = boostExicuter;
        _boardClearer = new(board);

    }

    public async UniTask Execute(IItem item)
    {
        var cell = _board[item.View.GetWorldPosition()];

        await _itemRemover.MoveNextStateItemIn(cell, 0);
        _boardClearer.ClearCell(cell);

        List<ICell> cells;

        if (item.SwapWith != null)
        {
            cells = GetCellsById(item.SwapWith.Id).Randomize().ToList();
        }
        else
        {
            cells = GetCellsById(GetMostPopularId()).Randomize().ToList();
        }

        await UniTask.WhenAll(cells.Select(cell => TryRemove(cell.GridPosition)).ToArray());

    }

    private int GetMostPopularId()
    {
        var cells = _board.Where(c => c.HasItem && c.IsBlocked == false)
                             .GroupBy(x => x.Item.Id)
                             .OrderByDescending(x => x.Count());

        if (cells.Count() > 0)
        {
            return cells.First().Key;
        }

        return default;
    }

    private IEnumerable<ICell> GetCellsById(int id)
    {
        return _board.Where(c => c.HasItem && c.IsBlocked == false).Where(c => c.Item.Id == id);
    }

    private async UniTask TryRemove(GridPosition position)
    {
        float removeTime = 0.08f;

        if (_board.IsPositionOnBoard(position))
        {
            var cell = _board[position];

            if (cell.HasItem == false)
                return;

            if (cell.Item is IBoostItem boost)
            {
                await ExequteAnotherBoost(boost);
            }

            await _itemRemover.MoveNextStateItemIn(cell, removeTime);
        }
    }

    private async UniTask ExequteAnotherBoost(IBoostItem boost)
    {
        await _boostExicuter.Execute(boost);
    }
}
