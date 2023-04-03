using Cysharp.Threading.Tasks;
using System;

public class NeighbourRemover
{
    private NeighbourFinder _neighbourFinder;
    private Func<ICell, UniTask> _remove;

    public NeighbourRemover(Board board,Func<ICell, UniTask> remove)
    {
        _remove = remove;
        _neighbourFinder = new(board);
    }

    public async UniTask RemoveNeighbours(GridPosition origin)
    {
        await UniTask.WhenAll(_neighbourFinder.FingNeighboursWithItem(origin).Select(n => _remove(n)));
    }
}
