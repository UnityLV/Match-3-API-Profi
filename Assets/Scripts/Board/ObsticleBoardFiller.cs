using System.Collections.Generic;

public class ObsticleBoardFiller
{
    private IEnumerable<ConfigFactory<int, IItem>> _obsticleFactoryes;
    private ItemInCellInjector _itemInCellInjector;

    public ObsticleBoardFiller(IEnumerable<ConfigFactory<int, IItem>> obsticleFactoryes)
    {
        _obsticleFactoryes = obsticleFactoryes;
        _itemInCellInjector = new();
    }

    public bool TryFill(ICell cell)
    {
        if (TryGetObsticleItem(cell.GridPosition.ColumnIndex, out IItem item))
        {
            _itemInCellInjector.InjectItemInCell(item, cell);
            return true;
        }
        return false;
    }

    private bool TryGetObsticleItem(int columnIndex, out IItem item)
    {
        item = null;

        foreach (var obsticleFactory in _obsticleFactoryes)
        {
            if (obsticleFactory.TryGet(columnIndex, out IItem searchItem))
            {
                item = searchItem;
            }
        }

        return item != null;
    }
}




