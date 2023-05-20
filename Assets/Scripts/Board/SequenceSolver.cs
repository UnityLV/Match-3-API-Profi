using Cysharp.Threading.Tasks;

public class SequenceSolver
{
    private SolveSlotsDetecor _solveSlotsDetecor;  
    private IBoardFaller _boardFaller;
    private IBoardFiller _boardFiller;
    private PositionMemory _positionMemory;
    private ItemNextStateMover _itemStateMover;
    private ObstilcleSolver _obstilcleSolver;
    private BoardClearer _boardClearer;

    public SequenceSolver(Workers workers)
    {
        _solveSlotsDetecor = workers.SolveSlotsDetecor;
        _boardFiller = workers.BoardFiller;

        _boardFaller = workers.BoardFaller;
        _boardClearer = workers.BoardClearer;
        _positionMemory = workers.PositionMemory;
        _itemStateMover = workers.ItemStateMover;
        _obstilcleSolver = workers.ObstilcleSolver;

    }

    public async UniTask FallAndSolve()
    {
        _boardClearer.ClearBordFromDeadItems();
        await _boardFaller.FallBoard();
        await _boardFiller.FillBoard();
        await SolveAllSequence();
    }

    public async UniTask SolveAllSequence()
    {
        var positions = _positionMemory.GetUppdatedGridPositions();

        while (_solveSlotsDetecor.TryGetBestSequence(positions, out var sequence))
        {
            await SolveSequence(sequence);

            positions = _positionMemory.GetUppdatedGridPositions();
        }

        _positionMemory.RememorizeAllPositions();
    }

    public async UniTask SolveSequence(MatchSequence sequence)
    {
        await _obstilcleSolver.SolveAll(sequence);
        await SolveItemInSequence(sequence);

        _boardClearer.ClearBordFromDeadItems();
        await _boardFaller.FallBoard();
        await _boardFiller.FillBoard();
    }

    private async UniTask SolveItemInSequence(MatchSequence sequence)
    {
        await _itemStateMover.SetNextStateSequence(sequence);

        bool isNeedToSpawnBoost = sequence.Type != SequenceTypes.Three;
        if (isNeedToSpawnBoost)
        {
            SpawnBoostIn(sequence);
        }
    }

    private void SpawnBoostIn(MatchSequence sequence)
    {
        var origin = sequence.Origin;
        var type = sequence.Type;

        _boardFiller.FillBoostInCell(ConvertCequenceToBoost(type), origin);
    }

    private BoostTypes ConvertCequenceToBoost(SequenceTypes boostTypes)
    {
        switch (boostTypes)
        {
            case SequenceTypes.FourHorisontal:
                return BoostTypes.Vertical;
            case SequenceTypes.FourVertical:
                return BoostTypes.Horizontal;
            case SequenceTypes.FiveLine:
                return BoostTypes.Rainbow;
            case SequenceTypes.TShape:
            case SequenceTypes.LShape:
                return BoostTypes.Bomb;
            case SequenceTypes.Square:
                return BoostTypes.Rocket;
            default:
                return BoostTypes.None;
        }
    }
}




