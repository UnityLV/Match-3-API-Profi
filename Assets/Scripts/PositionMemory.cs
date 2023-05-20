using System.Collections.Generic;

public class PositionMemory
{
    private Board _board;
    private int[,] _beforeChangeGridIds;

    private GridPosition _badPositon = new(-1, -1);

    public PositionMemory(Board board)
    {
        _board = board;
        InitMemoryGridIds();
    }

    public void InitMemoryGridIds()
    {
        _beforeChangeGridIds = new int[_board.Rows, _board.Columns];

        for (int rowIndex = 0; rowIndex < _board.Rows; rowIndex++)
        {
            for (int collumnIndex = 0; collumnIndex < _board.Columns; collumnIndex++)
            {
                if (_board[rowIndex, collumnIndex].HasItem)
                {
                    _beforeChangeGridIds[rowIndex, collumnIndex] = int.MinValue;
                }
            }
        }
    }

    public bool TryGetUppdatedGridPosition(out GridPosition position)
    {
        return (position = GetUppdatedGridPosition()) != _badPositon;
    }

    private GridPosition GetUppdatedGridPosition()
    {
        for (int rowIndex = 0; rowIndex < _board.Rows; rowIndex++)
        {
            for (int collumnIndex = 0; collumnIndex < _board.Columns; collumnIndex++)
            {
                if (_board[rowIndex, collumnIndex].HasItem)
                {
                    int oldSlotId = _beforeChangeGridIds[rowIndex, collumnIndex];
                    ICell currentSlot = _board[rowIndex, collumnIndex];
                    bool isNewItem = oldSlotId != currentSlot.Item.Id;
                    if (isNewItem)
                    {
                        _beforeChangeGridIds[rowIndex, collumnIndex] = currentSlot.Item.Id;
                        return currentSlot.GridPosition;
                    }
                }
            }
        }
        return _badPositon;
    }

    public List<GridPosition> GetUppdatedGridPositions()
    {
        List<GridPosition> postions = new();

        for (int rowIndex = 0; rowIndex < _board.Rows; rowIndex++)
        {
            for (int collumnIndex = 0; collumnIndex < _board.Columns; collumnIndex++)
            {
                if (_board[rowIndex, collumnIndex].HasItem)
                {
                    int oldSlotId = _beforeChangeGridIds[rowIndex, collumnIndex];
                    ICell currentCell = _board[rowIndex, collumnIndex];
                    bool isNewItem = oldSlotId != currentCell.Item.Id;
                    if (isNewItem)
                    {
                        postions.Add(currentCell.GridPosition);                        
                    }
                }
            }
        }

        return postions;
    }

    public void RememorizeAllPositions()
    {
        for (int rowIndex = 0; rowIndex < _board.Rows; rowIndex++)
        {
            for (int collumnIndex = 0; collumnIndex < _board.Columns; collumnIndex++)
            {
                if (_board[rowIndex, collumnIndex].HasItem)
                {
                    int oldSlotId = _beforeChangeGridIds[rowIndex, collumnIndex];
                    ICell currentSlot = _board[rowIndex, collumnIndex];
                    bool isNewItem = oldSlotId != currentSlot.Item.Id;

                    if (isNewItem)
                    {
                        _beforeChangeGridIds[rowIndex, collumnIndex] = currentSlot.Item.Id;                        
                    }
                }
            }
        }
    }

    public void Rememorize(MatchSequence sequence)
    {
        foreach (var cell in sequence)
        {
            int row = cell.GridPosition.RowIndex;
            int column = cell.GridPosition.ColumnIndex;

            int id = int.MinValue;
            if (cell.HasItem)
            {
                id = cell.Item.Id;
            }
            

            Rememorize(row, column, id);
        }
        
    }

    private void Rememorize(int row, int column, int id)
    {
        _beforeChangeGridIds[row, column] = id;
    }
}
