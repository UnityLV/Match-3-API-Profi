using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

public class Box2ObsticleSolver : IObsticleExecuter
{
    private Board _board;
    private GridPosition[] _lookupDirections = new[]
        {
            GridPosition.Up,
            GridPosition.Right,
            GridPosition.Down,
            GridPosition.Left
        };

    public Box2ObsticleSolver(Board board)
    {
        _board = board;
    }

    public async UniTask Execute(MatchSequence cequence)
    {
        HashSet<ICell> nearestCells = GetNearCellsToSequence(cequence);

        HashSet<IItem> boxItems = GetBox2Items(nearestCells);

        var x = boxItems;

        await UniTask.WhenAll(boxItems.Select(i => i.MoveNextState()).ToArray());
    }

    private HashSet<IItem> GetBox2Items(HashSet<ICell> nearestCells)
    {
        return nearestCells.Where(c => c.Item.State.Type == ItemStateTypes.Box2).Select(c => c.Item).ToHashSet();
    }

    private HashSet<ICell> GetNearCellsToSequence(MatchSequence cequence)
    {
        HashSet<ICell> nearCells = new();

        foreach (var cell in cequence)
        {
            AddNearCells(nearCells, cell);
        }

        return nearCells;
    }

    private void AddNearCells(HashSet<ICell> nearItems, ICell cell)
    {
        GridPosition position = cell.GridPosition;

        foreach (var direction in _lookupDirections)
        {
            var lookupPosition = position + direction;

            if (IsPositonAvalableToAddInSequence(lookupPosition) )
            {
                nearItems.Add(_board[lookupPosition]);
            }           
        }
    }


    private bool IsPositonAvalableToAddInSequence(GridPosition position)
    {
        if (IsOutOfBoard(position) || IsNoItem(_board[position]))
        {
            return false;
        }

        return true;
    }

    private bool IsNoItem(ICell cell) => cell.HasItem == false;

    private bool IsOutOfBoard(GridPosition lookupPosition) => _board.IsPositionOnBoard(lookupPosition) == false;
}
