using Cysharp.Threading.Tasks;
using UnityEngine.Events;

public class BoardSolver
{
    private IBoostExicuter _boostExicuter;
    private SequenceSolver _sequenceSolver;
    private ISwapSolwer _regularSwapSolver;
    private ISwapSolwer _boostSwapSolver;

    public event UnityAction CountedSwapMaked;

    public BoardSolver(Workers workers, ItemSwaper itemSwaper, IBoostExicuter boostExicuter)
    {
        _boostExicuter = boostExicuter;
        _sequenceSolver = new(workers);

        _regularSwapSolver = new RegularSwapSolwer(workers.SolveSlotsDetecor, itemSwaper, _sequenceSolver, () => CountedSwapMaked?.Invoke());
        _boostSwapSolver = new BoostSwapSolver(_boostExicuter, () => CountedSwapMaked?.Invoke());

    }

    public async UniTask SolveSwap(ICell selectedCell, ICell cell)
    {
        bool isBoostSwap = selectedCell.Item is IBoostItem || cell.Item is IBoostItem;

        if (isBoostSwap)
        {
            await _boostSwapSolver.SolveSwap(selectedCell, cell);
            await SolveBoard();
        }
        else
        {
            await _regularSwapSolver.SolveSwap(selectedCell, cell);
        }
    }


    public async UniTask SolveBoard()
    {
        await _sequenceSolver.FallAndSolve();
    }





}




