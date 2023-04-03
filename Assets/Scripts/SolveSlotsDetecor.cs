using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class SolveSlotsDetecor
{
    private IEnumerable<ISequenceDetector> _sequenceDetectors;
    private Board _board;

    public SolveSlotsDetecor(IEnumerable<ISequenceDetector> sequenceDetectors,Board board)
    {
        _sequenceDetectors = sequenceDetectors;
        _board = board;
    }

    public bool TryGetSequence(GridPosition position, out MatchSequence sequence)
    {
        var searchSlot = _board[position];

        if (IsBadSlot(searchSlot))
        {
            sequence = default;
            return false;
        }

        foreach (var detector in _sequenceDetectors)
        {
            if (detector.TryGetSequence(position, out sequence))
            {
                return true;
            }
        }

        sequence = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetBestSequence(IEnumerable<GridPosition> positions, out MatchSequence sequence)
    {
        foreach (var detector in _sequenceDetectors)
        {
            foreach (var position in positions)
            {
                var searchSlot = _board[position];

                if (IsBadSlot(searchSlot))
                {
                    continue;
                }

                if (detector.TryGetSequence(position, out sequence))
                {
                    return true;
                }
            }           
        }        

        sequence = default;
        return false;
    }


    private bool IsBadSlot(ICell searchSlot)
    {
        return searchSlot.Item.State.IsCanBeOriginOfSequence == false || searchSlot.Item.Id == GameConstatns.NullId;
    }

}
