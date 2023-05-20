using Cysharp.Threading.Tasks;
using UnityEngine.Events;

public class BoostSwapSolver : ISwapSolwer
{
    private IBoostExicuter _boostExicuter;
    private UnityAction _swapCallback;

    public BoostSwapSolver(IBoostExicuter boostExicuter, UnityAction swapCallback)
    {
        _boostExicuter = boostExicuter;
        _swapCallback = swapCallback;
    }

    public async UniTask SolveSwap(ICell selectedCell, ICell cell)
    {
        _swapCallback?.Invoke();

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
    }


}




