using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Board : IEnumerable<ICell>
{
    private CellFactory _cellFactory;

    private MidleCellFinder _midleCellFinder = new();

    private ICell[,] _cells;

    public Board(CellFactory cellFactory)
    {
        _cellFactory = cellFactory;

        Rows = _cellFactory.Height;
        Columns = _cellFactory.With;
    }

    public ICell this[GridPosition gridPosition] => _cells[gridPosition.RowIndex, gridPosition.ColumnIndex];
    public ICell this[Vector3 positon] => this[(int)positon.y, (int)positon.x];
    public ICell this[int rowIndex, int columnIndex] => _cells[rowIndex, columnIndex];
    public int Rows { get; private set; }
    public int Columns { get; private set; }
    public Vector3 CenterPoint => _midleCellFinder.Find(_cells);


    public void CreateCells()
    {
        _cells = new ICell[Rows, Columns];

        for (int rowIndex = 0; rowIndex < Rows; rowIndex++)
        {
            for (int collumnIndex = 0; collumnIndex < Columns; collumnIndex++)
            {
                var cellposition = new GridPosition(rowIndex, collumnIndex);
                _cells[rowIndex, collumnIndex] = _cellFactory.Get(cellposition);
            }
        }
    }

    public IEnumerator GetEnumerator()
    {
        return _cells.GetEnumerator();
    }

    IEnumerator<ICell> IEnumerable<ICell>.GetEnumerator()
    {
        foreach (var cell in _cells)
        {
            yield return cell;
        }
    }
}

