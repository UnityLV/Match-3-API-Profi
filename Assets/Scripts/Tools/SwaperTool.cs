using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class SwaperTool : ITool
{
    private UniTaskCompletionSource<(ICell, ICell)> _uniTaskCompletionSource;
    private CancellationTokenSource _cancellationTokenSource;
    private CellSelector _cellSelector;
    private BoardSolver _boardSolver;
    private ItemSwaper _itemSwaper;

    public event UnityAction ExecuteStarted;

    public SwaperTool(CellSelector cellSelector, BoardSolver boardSolver, ItemSwaper itemSwaper)
    {
        _cellSelector = cellSelector;
        _boardSolver = boardSolver;
        _itemSwaper = itemSwaper;
    }

    public async UniTask Execute()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _uniTaskCompletionSource = new UniTaskCompletionSource<(ICell, ICell)>();
        _cellSelector.SelectSecondCell += OnSelectTwoCells;

        try
        {
            (ICell firstCell, ICell secondCell) = await WaitForSelectTwoCellsAsync();

            ExecuteStarted?.Invoke();

            await _itemSwaper.SwapItems(firstCell, secondCell);

            await _boardSolver.SolveBoard();
        }
        catch (OperationCanceledException)
        {
            Debug.Log("RemoverTool execution was cancelled");
        }
    }

    private void OnSelectTwoCells(ICell firstCell, ICell secondCell)
    {
        _uniTaskCompletionSource.TrySetResult((firstCell, secondCell));
    }

    public async UniTask<(ICell, ICell)> WaitForSelectTwoCellsAsync()
    {
        return await _uniTaskCompletionSource.Task.AttachExternalCancellation(_cancellationTokenSource.Token);
    }

    public void Forget()
    {
        _cellSelector.SelectSecondCell -= OnSelectTwoCells;
        _cancellationTokenSource?.Cancel();
    }

    

    public void Dispose()
    {
        _cellSelector.SelectSecondCell -= OnSelectTwoCells;
        _cancellationTokenSource?.Dispose();
    }
}





