public class SequenceFiveLineDetector : BaseSequenceDetector
{
    public SequenceFiveLineDetector(Board board) : base(board)
    {
        Type = SequenceTypes.FiveLine;
        SequenceLength = 5;
        SequenceLookupDirections = new[]
        {
            new[] { GridPosition.Right,GridPosition.Right + GridPosition.Right, GridPosition.Right + GridPosition.Right + GridPosition.Right,GridPosition.Right + GridPosition.Right + GridPosition.Right + GridPosition.Right},
            new[] { GridPosition.Left, GridPosition.Right,GridPosition.Right + GridPosition.Right, GridPosition.Right + GridPosition.Right +GridPosition.Right},
            new[] { GridPosition.Left,GridPosition.Left + GridPosition.Left,GridPosition.Right, GridPosition.Right + GridPosition.Right},
            new[] { GridPosition.Left, GridPosition.Left + GridPosition.Left , GridPosition.Left + GridPosition.Left + GridPosition.Left,GridPosition.Right},
            new[] { GridPosition.Left, GridPosition.Left + GridPosition.Left, GridPosition.Left + GridPosition.Left + GridPosition.Left,GridPosition.Left + GridPosition.Left + GridPosition.Left + GridPosition.Left},

            new[] { GridPosition.Down,GridPosition.Down + GridPosition.Down, GridPosition.Down + GridPosition.Down + GridPosition.Down,GridPosition.Down + GridPosition.Down + GridPosition.Down + GridPosition.Down},
            new[] { GridPosition.Up, GridPosition.Down,GridPosition.Down + GridPosition.Down, GridPosition.Down + GridPosition.Down +GridPosition.Down},
            new[] { GridPosition.Up,GridPosition.Up + GridPosition.Up,GridPosition.Down, GridPosition.Down + GridPosition.Down},
            new[] { GridPosition.Up, GridPosition.Up + GridPosition.Up , GridPosition.Up + GridPosition.Up + GridPosition.Up,GridPosition.Down},
            new[] { GridPosition.Up, GridPosition.Up + GridPosition.Up, GridPosition.Up + GridPosition.Up + GridPosition.Up,GridPosition.Up + GridPosition.Up + GridPosition.Up + GridPosition.Up},

        };
    }

}
