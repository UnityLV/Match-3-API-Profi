using System.Collections.Generic;
using System.Runtime.CompilerServices;

public abstract class BaseSequenceDetector : ISequenceDetector
{
    protected GridPosition[][] SequenceLookupDirections;
    protected SequenceTypes Type;
    protected int SequenceLength;
    private Board _board;

    protected BaseSequenceDetector(Board board)
    {
        _board = board;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetSequence(GridPosition position, out MatchSequence sequence)
    {
        var sequenceOriginCell = _board[position];

        if (sequenceOriginCell.HasItem == false)
        {
            sequence = default;
            return false;
        }

        var resultGridCells = new List<ICell>(SequenceLength);

        foreach (var lookupDirections in SequenceLookupDirections)
        {
            TryFindSequence(sequenceOriginCell, resultGridCells, lookupDirections);

            if (IsFoundAllCellForSequence(resultGridCells))
            {
                resultGridCells.Add(sequenceOriginCell);
                break;
            }

            resultGridCells.Clear();
        }

        sequence = new(resultGridCells);
        sequence.Type = Type;
        sequence.Origin = _board[position];
        return resultGridCells.Count > 0;
    }

    private void TryFindSequence(ICell searchCell, List<ICell> resultGridCells, GridPosition[] lookupDirections)
    {
        GridPosition position = searchCell.GridPosition;

        foreach (var lookupDirection in lookupDirections)
        {
            var lookupPosition = position + lookupDirection;

            if (IsCellAvalableToAddInSequence(lookupPosition) == false)
            {
                break;
            }

            var lookupGridCell = _board[lookupPosition];

            if (IsSameId(searchCell, lookupGridCell))
            {
                resultGridCells.Add(lookupGridCell);
            }
        }
    }

    private bool IsCellAvalableToAddInSequence(GridPosition position)
    {
        if (IsOutOfBoard(position) || IsNoItem(_board[position]))
        {
            return false;
        }

        return true;
    }

    private bool IsFoundAllCellForSequence(List<ICell> resultGridSlots) => resultGridSlots.Count == SequenceLength - 1;

    private bool IsSameId(ICell searchSlot, ICell lookupGridSlot) => lookupGridSlot.Item.Id == searchSlot.Item.Id;

    private bool IsNoItem(ICell cell) => cell.HasItem == false;

    private bool IsOutOfBoard(GridPosition lookupPosition) => _board.IsPositionOnBoard(lookupPosition) == false;
}
