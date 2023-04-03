public class BoostBoardFiller
{
    private ItemInCellInjector _itemInCellInjector;
    private ConfigFactory<BoostTypes, IBoostItem> _boostFactory;

    public BoostBoardFiller(ConfigFactory<BoostTypes, IBoostItem> boostFactory)
    {
        _boostFactory = boostFactory;       
        _itemInCellInjector = new();
    }

    public void FillBoostInCell(BoostTypes type, ICell cell)
    {
        IBoostItem item = _boostFactory.Get(type);

        _itemInCellInjector.InjectItemInCell(item, cell);
    }
}




