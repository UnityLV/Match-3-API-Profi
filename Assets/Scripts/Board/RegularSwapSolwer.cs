using Cysharp.Threading.Tasks;
using UnityEngine.Events;

public class RegularSwapSolwer : ISwapSolwer
{
    private SolveSlotsDetecor _solveSlotsDetecor;
    private ItemSwaper _itemSwaper;
    private SequenceSolver _sequenceSolver;
    private UnityAction _swapCallback;

    public RegularSwapSolwer(SolveSlotsDetecor solveSlotsDetecor, ItemSwaper itemSwaper, SequenceSolver sequenceSolver, UnityAction swapCallback)
    {
        _solveSlotsDetecor = solveSlotsDetecor;
        _itemSwaper = itemSwaper;    

        _sequenceSolver = sequenceSolver;
        _swapCallback = swapCallback;
    }

    public async UniTask SolveSwap(ICell selectedCell, ICell cell)
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
        _swapCallback?.Invoke();

        await _sequenceSolver.SolveSequence(sequence);

        await _sequenceSolver.SolveAllSequence();
    }

  

}




