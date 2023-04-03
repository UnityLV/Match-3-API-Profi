public class SequenceFourVerticalDetector : BaseSequenceDetector
{   

    public SequenceFourVerticalDetector(Board board) : base(board)
    {
        Type = SequenceTypes.FourVertical;
        SequenceLength = 4;
        SequenceLookupDirections = new[]
        {
            new[] { GridPosition.Down, GridPosition.Down + GridPosition.Down, GridPosition.Down + GridPosition.Down  + GridPosition.Down},
            new[] { GridPosition.Up, GridPosition.Up + GridPosition.Up, GridPosition.Up + GridPosition.Up + GridPosition.Up },

            new[] { GridPosition.Down, GridPosition.Up, GridPosition.Up + GridPosition.Up  },
            new[] { GridPosition.Up, GridPosition.Down, GridPosition.Down + GridPosition.Down  },

        };
    }

}
