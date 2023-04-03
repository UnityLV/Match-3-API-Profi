using System.Collections.Generic;

public class SequenceThreeDetector : BaseSequenceDetector
{
    

    public SequenceThreeDetector(Board board) : base(board)
    {
        Type = SequenceTypes.Three;
        SequenceLength = 3;

       SequenceLookupDirections = new[]
        {
            new[] { GridPosition.Up, GridPosition.Up + GridPosition.Up },
            new[] { GridPosition.Right, GridPosition.Right + GridPosition.Right },
            new[] { GridPosition.Left, GridPosition.Left + GridPosition.Left },
            new[] { GridPosition.Down, GridPosition.Down + GridPosition.Down },

            new[] { GridPosition.Right, GridPosition.Left},
            new[] { GridPosition.Up, GridPosition.Down },

        };
    }

}
