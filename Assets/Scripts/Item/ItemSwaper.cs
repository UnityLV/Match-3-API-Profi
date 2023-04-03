using Cysharp.Threading.Tasks;
using UnityEngine;

public class ItemSwaper : MonoBehaviour
{
    private float _moveTime = 0.2f;
    private bool _isCanSwap = true;

    public bool IsCanSwap(ICell cell1, ICell cell2)
    {
        if (cell1 == null || cell2 == null)
            return false;

        if (GameConstatns.IsNeedToCheckAvableMove &&
            IsNearPosition(cell1.GridPosition, cell2.GridPosition) == false)
        {            
            return false;
        }

        bool isAvalableForItems = cell1.CanContainsItem && cell2.CanContainsItem;
        bool isHasItems = cell1.HasItem && cell2.HasItem;

        bool isItemCanMove = false;

        if (isHasItems)
        {
            isItemCanMove = cell1.Item.State.Movable && cell2.Item.State.Movable;
        }


        if (_isCanSwap && isAvalableForItems && isHasItems && isItemCanMove)
        {
            return true;
        }
        return false;
    }

    private bool IsNearPosition(GridPosition position, GridPosition gridPosition)
    {
        if (position + GridPosition.Up == gridPosition ||
            position + GridPosition.Down == gridPosition ||
            position + GridPosition.Right == gridPosition ||
            position + GridPosition.Left == gridPosition)
        {
            return true;
        }
        return false;
    }    

    public async UniTask SwapItems(ICell cell1, ICell cell2)
    {
        var item1 = cell1.Item;
        var item2 = cell2.Item;

        var position1 = cell1.View.GetWorldPosition();
        var position2 = cell2.View.GetWorldPosition();

        cell1.Item = item2;
        cell2.Item = item1;

        item1.OnSwap();
        item2.OnSwap();             

        await UniTask.WhenAll(item1.View.MoveOn(_moveTime, path: position2),
            item2.View.MoveOn(_moveTime, path: position1));

        SetInputCountdown();
    }

    private async UniTask SetInputCountdown()
    {
        _isCanSwap = false;
        await UniTask.Delay((int)(_moveTime * 1000f));
        _isCanSwap = true;
    }
}