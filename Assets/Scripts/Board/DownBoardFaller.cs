using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class DownBoardFaller : IBoardFaller
{
    private Board _board;

    private int _rows;
    private int _collumns;

    private int _fallRowDelay = 30;

    public DownBoardFaller(Board board)
    {
        _board = board;
        _rows = _board.Rows;
        _collumns = _board.Columns;
    }

    public async UniTask FallBoard()
    {
        for (int rowIndex = 0; rowIndex < _rows; rowIndex++)
        {
            for (int collumnIndex = 0; collumnIndex < _collumns; collumnIndex++)
            {
                TryFallOnCell(rowIndex, collumnIndex);
            }
            await UniTask.Delay(_fallRowDelay);
        }

    }

    private void TryFallOnCell(int rowIndex, int collumnIndex)
    {
        var fallingOnCell = _board[rowIndex, collumnIndex];

        if (fallingOnCell.CanSetItem)
        {
            if (TryGetFallPath(rowIndex, collumnIndex, out var path))
            {
                Fill(fallingOnCell, path);
            }
        }
    }

    private void Fill(ICell fillingCell, List<Vector3> path)
    {
        var fallingCell = _board[path[0]];

        bool isAvlable = fallingCell.HasItem && fallingCell.IsBlocked == false && fallingCell.Item.State.Movable;
        if (isAvlable)
        {
            FallOnCell(fillingCell, path, fallingCell);
        }
    }

    private void FallOnCell(ICell fillingCell, List<Vector3> path, ICell fallingCell)
    {
        float fallTime = 0.05f;

        bool isFallOnOneCell = path.Count == 2;
        if (isFallOnOneCell)
        {
            float oneCellMoveTimeScaler = 2;
            fallTime *= oneCellMoveTimeScaler;
        }

        fillingCell.Item = fallingCell.Item;

        fillingCell.Item.View.SetWorldPosition(path[0]);
        fillingCell.Item.View.MoveOn(fallTime, path: path.ToArray());

        fallingCell.Clear();
    }

    private bool TryGetFallPath(int rowIndex, int collumnIndex, out List<Vector3> path)
    {
        return (path = FindFallPathOnEmptyCell(rowIndex, collumnIndex)).Count > 0;
    }

    private List<Vector3> FindFallPathOnEmptyCell(int rowIndex, int collumnIndex)
    {
        List<Vector3> fallPath = new();

        for (int cellRow = rowIndex; cellRow < _rows; cellRow++)
        {
            var candidateItemCellToFall = _board[cellRow, collumnIndex];

            bool notContainsItem = candidateItemCellToFall.HasItem == false;
            bool isEmptyOnPath = notContainsItem && candidateItemCellToFall.IsBlocked == false;
            if (isEmptyOnPath)
            {
                fallPath.Add(candidateItemCellToFall.View.GetWorldPosition());
                continue;
            }

            bool isCellWithItemOnPath = candidateItemCellToFall.HasItem && candidateItemCellToFall.IsBlocked == false;

            if (isCellWithItemOnPath)
                fallPath.Add(candidateItemCellToFall.View.GetWorldPosition());
            else
                fallPath.Clear();

            break;

        }

        fallPath.Reverse();

        return fallPath;
    }
}
