public class SquareDetector : BaseSequenceDetector
{
    public SquareDetector(Board board) : base(board)
    {
        Type = SequenceTypes.Square;
        SequenceLength = 4;
        SequenceLookupDirections = new[]
        {
            new[] { GridPosition.Up,GridPosition.Right + GridPosition.Up,GridPosition.Right},
            new[] { GridPosition.Right,GridPosition.Down + GridPosition.Right,GridPosition.Down},
            new[] { GridPosition.Down,GridPosition.Left + GridPosition.Down,GridPosition.Left},
            new[] { GridPosition.Left,GridPosition.Up + GridPosition.Left,GridPosition.Up},
        };
    }

}
