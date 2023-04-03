public class TShapeDetector : BaseSequenceDetector
{
    public TShapeDetector(Board board) : base(board)
    {
        Type = SequenceTypes.TShape;
        SequenceLength = 5;
        SequenceLookupDirections = new[]
        {

            new[] {GridPosition.Right, GridPosition.Right + GridPosition.Right, GridPosition.Right + GridPosition.Down,GridPosition.Right + GridPosition.Down + GridPosition.Down},
            new[] {GridPosition.Left,GridPosition.Right,GridPosition.Down,GridPosition.Down + GridPosition.Down},
            new[] {GridPosition.Left,GridPosition.Left + GridPosition.Left, GridPosition.Left + GridPosition.Down,GridPosition.Left + GridPosition.Down + GridPosition.Down},
            new[] {GridPosition.Up,GridPosition.Up + GridPosition.Left,GridPosition.Up + GridPosition.Right,GridPosition.Down},
            new[] {GridPosition.Up,GridPosition.Up + GridPosition.Up,GridPosition.Up + GridPosition.Up + GridPosition.Left,GridPosition.Up + GridPosition.Up + GridPosition.Right},

            new[] { GridPosition.Down, GridPosition.Down + GridPosition.Down, GridPosition.Down + GridPosition.Left,GridPosition.Down + GridPosition.Left + GridPosition.Left},
            new[] {GridPosition.Up,GridPosition.Down,GridPosition.Left,GridPosition.Left + GridPosition.Left},
            new[] {GridPosition.Up,GridPosition.Up + GridPosition.Up, GridPosition.Up + GridPosition.Left,GridPosition.Up + GridPosition.Left + GridPosition.Left},
            new[] {GridPosition.Right,GridPosition.Right + GridPosition.Up,GridPosition.Right + GridPosition.Down,GridPosition.Left},
            new[] {GridPosition.Right,GridPosition.Right + GridPosition.Right,GridPosition.Right + GridPosition.Right + GridPosition.Up,GridPosition.Right + GridPosition.Right + GridPosition.Down},

            new[] { GridPosition.Left, GridPosition.Left + GridPosition.Left, GridPosition.Left + GridPosition.Up,GridPosition.Left + GridPosition.Up + GridPosition.Up},
            new[] {GridPosition.Right,GridPosition.Left,GridPosition.Up,GridPosition.Up + GridPosition.Up},
            new[] {GridPosition.Right,GridPosition.Right + GridPosition.Right, GridPosition.Right + GridPosition.Up,GridPosition.Right + GridPosition.Up + GridPosition.Up},
            new[] {GridPosition.Down,GridPosition.Down + GridPosition.Right,GridPosition.Down + GridPosition.Left,GridPosition.Up},
            new[] {GridPosition.Down,GridPosition.Down + GridPosition.Down,GridPosition.Down + GridPosition.Down + GridPosition.Right,GridPosition.Down + GridPosition.Down + GridPosition.Left},

            new[] { GridPosition.Up, GridPosition.Up + GridPosition.Up, GridPosition.Up + GridPosition.Right,GridPosition.Up + GridPosition.Right + GridPosition.Right},
            new[] {GridPosition.Down,GridPosition.Up,GridPosition.Right,GridPosition.Right + GridPosition.Right},
            new[] {GridPosition.Down,GridPosition.Down + GridPosition.Down, GridPosition.Down + GridPosition.Right,GridPosition.Down + GridPosition.Right + GridPosition.Right},
            new[] {GridPosition.Left,GridPosition.Left + GridPosition.Down,GridPosition.Left + GridPosition.Up,GridPosition.Right},
            new[] {GridPosition.Left,GridPosition.Left + GridPosition.Left,GridPosition.Left + GridPosition.Left + GridPosition.Down,GridPosition.Left + GridPosition.Left + GridPosition.Up},
        };
    }

}
