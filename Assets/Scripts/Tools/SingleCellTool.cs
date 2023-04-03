using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public abstract class SingleCellTool : ITool, IDisposable
{
    private UniTaskCompletionSource<ICell> _uniTaskCompletionSource;
    private readonly CellSelector _cellSelector;
    private readonly BoardSolver _boardSolver;
    private CancellationTokenSource _cancellationTokenSource;    

    public event UnityAction ExecuteStarted;

    public SingleCellTool(CellSelector cellSelector, BoardSolver boardSolver)
    {
        _cellSelector = cellSelector;
        _boardSolver = boardSolver;
    }

    public async UniTask Execute()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _uniTaskCompletionSource = new UniTaskCompletionSource<ICell>();
        _cellSelector.SelectFirsCell += OnSelectFirstCell;

        try
        {
            ICell cell = await WaitForSelectFirstCellAsync();

            ExecuteStarted?.Invoke();

            await ToolAction(cell);

            await _boardSolver.SolveBoard();

        }
        catch (OperationCanceledException)
        {
            Debug.Log("RemoverTool execution was cancelled");

        }
    }

    protected abstract UniTask ToolAction(ICell cell);

    private void OnSelectFirstCell(ICell cell)
    {
        _uniTaskCompletionSource.TrySetResult(cell);
    }

    public async UniTask<ICell> WaitForSelectFirstCellAsync()
    {
        return await _uniTaskCompletionSource.Task.AttachExternalCancellation(_cancellationTokenSource.Token);
    }

    public void Forget()
    {
        _cellSelector.SelectFirsCell -= OnSelectFirstCell;
        _cancellationTokenSource?.Cancel();
    }

    public void Dispose()
    {
        _cellSelector.SelectFirsCell -= OnSelectFirstCell;
        _cancellationTokenSource?.Dispose();
    }
}
