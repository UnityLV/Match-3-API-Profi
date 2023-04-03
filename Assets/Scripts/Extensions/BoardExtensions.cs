using System.Runtime.CompilerServices;

public static class BoardExtensions
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPositionOnBoard(this Board Board, GridPosition gridPosition)
    {
        return IsPositionOnBoard(gridPosition, Board.Rows, Board.Columns);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPositionOnBoard(GridPosition gridPosition, int rowCount, int columnCount)
    {
        return gridPosition.RowIndex >= 0 &&
               gridPosition.RowIndex < rowCount &&
               gridPosition.ColumnIndex >= 0 &&
               gridPosition.ColumnIndex < columnCount;
    }
}
