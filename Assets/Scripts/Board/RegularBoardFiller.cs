using Cysharp.Threading.Tasks;
using UnityEngine;

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




