using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;



public class RegularBoardFiller
{
    private ConfigFactory<int, IItem> _itemFactory;
    private ItemInCellInjector _itemInCellInjector;

    public RegularBoardFiller(ConfigFactory<int, IItem> itemFactory)
    {
        _itemFactory = itemFactory;
        _itemInCellInjector = new();
    }

    public async UniTask TryFillCellRegularItem(ICell cell, params Vector3[] path)
    {
        bool isNeedToFill = cell.HasItem == false && cell.CanContainsItem;
        if (isNeedToFill)
        {
            await FillCellRegularItem(cell, path);
        }
    }
    private async UniTask FillCellRegularItem(ICell cell, params Vector3[] path)
    {
        var item = _itemFactory.Get(cell.GridPosition.ColumnIndex);
        await _itemInCellInjector.InjectItemInCell(item, cell, path);
    }
}













public class BoardSolver
{
    private SolveSlotsDetecor _solveSlotsDetecor;
    private ItemSwaper _itemSwaper;
    private IBoardFaller _boardFaller;
    private IBoardFiller _boardFiller;
    private BoostExicuter _boostExicuter;
    private PositionMemory _positionMemory;
    private ItemNextStateMover _itemStateMover;
    private ObstilcleSolver _obstilcleSolver;
    private BoardClearer _boardClearer;

    public event UnityAction CountedSwapMaked;

    public BoardSolver(SolveSlotsDetecor solveSlotsDetecor, ItemSwaper itemSwaper, IBoardFiller boardFiller, Board board, BoostExicuter boostExicuter)
    {
        _solveSlotsDetecor = solveSlotsDetecor;
        _itemSwaper = itemSwaper;
        _boardFiller = boardFiller;

        _boardFaller = new DownBoardFaller(board);
        _boardClearer = new(board);
        _positionMemory = new(board);
        _itemStateMover = new();
        _boostExicuter = boostExicuter;
        _obstilcleSolver = new(board);
    }

    public async UniTask SolveSwap(ICell selectedCell, ICell cell)
    {
        bool isBoostSwap = selectedCell.Item is IBoostItem || cell.Item is IBoostItem;

        if (isBoostSwap)
        {
            await SolveBoostSwap(selectedCell, cell);
        }
        else
        {
            await SolveRegularSwap(selectedCell, cell);
        }
    }

    private async UniTask SolveBoostSwap(ICell selectedCell, ICell cell)
    {
        CountedSwapMaked?.Invoke();

        if (selectedCell.Item is IBoostItem selectedBoost)
        {
            selectedBoost.SwapWith = cell.Item;
            await _boostExicuter.Execute(selectedBoost);//ToDo: await нужно ожидать паралельно у обоих бустов?
        }

        if (cell.Item is IBoostItem boost)
        {
            boost.SwapWith = selectedCell.Item;
            await _boostExicuter.Execute(boost);//ToDo: await нужно ожидать паралельно у обоих бустов?
        }

        _boardClearer.ClearBordFromDeadItems();

        await _boardFaller.FallBoard();
        await _boardFiller.FillBoard();
        await SolveAllSequence();
    }

    public async UniTask SolveBoard()
    {
        _boardClearer.ClearBordFromDeadItems();
        await _boardFaller.FallBoard();
        await _boardFiller.FillBoard();
        await SolveAllSequence();
    }

    private async UniTask SolveRegularSwap(ICell selectedCell, ICell cell)
    {
        bool isMatchExist = _solveSlotsDetecor.TryGetSequence(selectedCell.GridPosition, out var sequence) ||
                                    _solveSlotsDetecor.TryGetSequence(cell.GridPosition, out sequence);

        if (isMatchExist)
        {
            await CreateRegularSwapWithExistingMath(sequence);
        }
        else
        {
            await _itemSwaper.SwapItems(cell, selectedCell);
        }
    }

    private async UniTask CreateRegularSwapWithExistingMath(MatchSequence sequence)
    {
        CountedSwapMaked?.Invoke();

        await SolveSequence(sequence);

        await SolveAllSequence();
    }

    private async UniTask SolveAllSequence()
    {
        var positions = _positionMemory.GetUppdatedGridPositions();

        while (_solveSlotsDetecor.TryGetBestSequence(positions, out var sequence))
        {
            await SolveSequence(sequence);

            positions = _positionMemory.GetUppdatedGridPositions();
        }

        _positionMemory.RememorizeAllPositions();
    }

    private async UniTask SolveSequence(MatchSequence sequence)
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




