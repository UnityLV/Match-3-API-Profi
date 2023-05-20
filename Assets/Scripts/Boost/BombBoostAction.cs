using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombBoostAction : IBoostAction
{
    private const float BombRadius = 2f;
    private Board _board;
    private ItemNextStateMover _itemRemover;
    private IBoostExicuter _boostExicuter;
    private BoardClearer _boardClearer;

    public BombBoostAction(Board board,  IBoostExicuter boostExicuter)
    {
        _board = board;
        _itemRemover = new();
        _boostExicuter = boostExicuter;
        _boardClearer = new(board);
    }

    public async UniTask Execute(IItem item)
    {
        var cell = _board[item.View.GetWorldPosition()];

        Vector2 bombPosition = cell.View.GetWorldPosition();

        await _itemRemover.MoveNextStateItemIn(cell, 0);
        _boardClearer.ClearCell(cell);

        if (TryGetCells(bombPosition, out var cells))
        {
            await UniTask.WhenAll(cells.Select(cell => TryRemove(cell.GridPosition)).ToArray());
        }
    }

    private bool TryGetCells(Vector2 bombPosition, out List<ICell> cells)
    {
        return (cells = GetCells(bombPosition)).Count > 0;
    }

    private List<ICell> GetCells(Vector2 bombPosition)
    {
        var colliders = Physics2D.OverlapCircleAll(bombPosition, BombRadius);

        var cells = new List<ICell>();

        foreach (var collider in colliders)
        {
            if (collider.transform.parent != null)
            {
                ICell cell = collider.transform.GetComponentInParent<ICell>();
                if (cell != null)
                {
                    cells.Add(cell);
                }
            }
        }

        return cells;
    }

    private async UniTask TryRemove(GridPosition position)
    {
        float removeTime = 0.01f;

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
