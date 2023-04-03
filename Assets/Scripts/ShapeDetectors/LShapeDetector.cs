public class LShapeDetector : BaseSequenceDetector
{
    public LShapeDetector(Board board) : base(board)
    {
        Type = SequenceTypes.LShape;
        SequenceLength = 5;
        SequenceLookupDirections = new[]
        {
            new[] { GridPosition.Up, GridPosition.Up + GridPosition.Up,GridPosition.Up + GridPosition.Up + GridPosition.Right,GridPosition.Up + GridPosition.Up + GridPosition.Right + GridPosition.Right},

            new[]{ GridPosition.Down,GridPosition.Up,GridPosition.Up + GridPosition.Right,GridPosition.Up + GridPosition.Right + GridPosition.Right},

            new[] {GridPosition.Down,GridPosition.Down + GridPosition.Down,GridPosition.Right,GridPosition.Right + GridPosition.Right },

            new[] {GridPosition.Right,GridPosition.Left,GridPosition.Left + GridPosition.Down,GridPosition.Left + GridPosition.Down + GridPosition.Down},

            new[] {GridPosition.Left,GridPosition.Left + GridPosition.Left,GridPosition.Left + GridPosition.Left + GridPosition.Down,GridPosition.Left + GridPosition.Left + GridPosition.Down + GridPosition.Down},


            new[] { GridPosition.Right, GridPosition.Right + GridPosition.Right,GridPosition.Right + GridPosition.Right + GridPosition.Down,GridPosition.Right + GridPosition.Right + GridPosition.Down + GridPosition.Down},

            new[]{ GridPosition.Left,GridPosition.Right,GridPosition.Right + GridPosition.Down,GridPosition.Right + GridPosition.Down + GridPosition.Down},

            new[] {GridPosition.Left,GridPosition.Left + GridPosition.Left,GridPosition.Down,GridPosition.Down + GridPosition.Down },

            new[] {GridPosition.Down,GridPosition.Up,GridPosition.Up + GridPosition.Left,GridPosition.Up + GridPosition.Left + GridPosition.Left},

            new[] {GridPosition.Up,GridPosition.Up + GridPosition.Up,GridPosition.Up + GridPosition.Up + GridPosition.Left,GridPosition.Up + GridPosition.Up + GridPosition.Left + GridPosition.Left},


            new[] { GridPosition.Down, GridPosition.Down + GridPosition.Down,GridPosition.Down + GridPosition.Down + GridPosition.Left,GridPosition.Down + GridPosition.Down + GridPosition.Left + GridPosition.Left},

            new[]{ GridPosition.Up,GridPosition.Down,GridPosition.Down + GridPosition.Left,GridPosition.Down + GridPosition.Left + GridPosition.Left},

            new[] {GridPosition.Up,GridPosition.Up + GridPosition.Up,GridPosition.Left,GridPosition.Left + GridPosition.Left },

            new[] {GridPosition.Left,GridPosition.Right,GridPosition.Right + GridPosition.Up,GridPosition.Right + GridPosition.Up + GridPosition.Up},

            new[] {GridPosition.Right,GridPosition.Right + GridPosition.Right,GridPosition.Right + GridPosition.Right + GridPosition.Up,GridPosition.Right + GridPosition.Right + GridPosition.Up + GridPosition.Up},


            new[] { GridPosition.Left, GridPosition.Left + GridPosition.Left,GridPosition.Left + GridPosition.Left + GridPosition.Up,GridPosition.Left + GridPosition.Left + GridPosition.Up + GridPosition.Up},

            new[]{ GridPosition.Right,GridPosition.Left,GridPosition.Left + GridPosition.Up,GridPosition.Left + GridPosition.Up + GridPosition.Up},

            new[] {GridPosition.Right,GridPosition.Right + GridPosition.Right,GridPosition.Up,GridPosition.Up + GridPosition.Up },

            new[] {GridPosition.Up,GridPosition.Down,GridPosition.Down + GridPosition.Right,GridPosition.Down + GridPosition.Right + GridPosition.Right},

            new[] {GridPosition.Down,GridPosition.Down + GridPosition.Down,GridPosition.Down + GridPosition.Down + GridPosition.Right,GridPosition.Down + GridPosition.Down + GridPosition.Right + GridPosition.Right},

        };
    }

}
