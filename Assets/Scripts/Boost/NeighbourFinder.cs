using System.Collections.Generic;
using System.Linq;

public class NeighbourFinder
{
    private Board _board;

    public NeighbourFinder(Board board)
    {
        _board = board;
    }

    private readonly GridPosition[] _neighboursDirectons = new GridPosition[] { GridPosition.Up, GridPosition.Right, GridPosition.Down, GridPosition.Left };

    public IEnumerable<ICell> FingNeighboursWithItem(GridPosition origin)
    {
        return FingNeighbours(origin).Where(n => n.HasItem);
    }

    private IEnumerable<ICell> FingNeighbours(GridPosition origin)
    {
        foreach (var neighbourDirection in _neighboursDirectons)
        {
            var neighbourPosition = neighbourDirection + origin;

            if (_board.IsPositionOnBoard(neighbourPosition))
            {
                yield return _board[neighbourPosition];
            }
        }
    }
}
