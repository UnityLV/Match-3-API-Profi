public class SequenceFourHorisontalDetector : BaseSequenceDetector
{
    public SequenceFourHorisontalDetector(Board board) : base(board)
    {
        Type = SequenceTypes.FourHorisontal;
        SequenceLength = 4;
        SequenceLookupDirections = new[]
        {
            new[] { GridPosition.Right, GridPosition.Right + GridPosition.Right, GridPosition.Right + GridPosition.Right  + GridPosition.Right},
            new[] { GridPosition.Left, GridPosition.Left + GridPosition.Left, GridPosition.Left + GridPosition.Left + GridPosition.Left },

            new[] { GridPosition.Right, GridPosition.Left, GridPosition.Left + GridPosition.Left  },
            new[] { GridPosition.Left, GridPosition.Right, GridPosition.Right + GridPosition.Right  },

        };
    }

}
