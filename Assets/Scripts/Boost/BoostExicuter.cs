using Cysharp.Threading.Tasks;



public class BoostExicuter
{
    private IBoostAction _horisontal;
    private IBoostAction _vertical;
    private IBoostAction _bomb;
    private IBoostAction _rainbow;
    private IBoostAction _rocket;
    private IBoostAction _xlines;

    private BoardClearer _boardClearer;

    public BoostExicuter(ItemNextStateMover itemRemover, Board board, GoalCellOnBoardFinder goalItemFinder)
    {
        _horisontal = new HorizontalLineRemover(board, itemRemover, this);
        _vertical = new VerticalLineRemover(board, itemRemover, this);
        _bomb = new BombBoostAction(board, itemRemover, this);
        _rainbow = new RainbowBoostAction(board, itemRemover, this);
        _rocket = new RocketBoostAction(board, goalItemFinder, itemRemover, this);
        _xlines = new XLineRemover(board, itemRemover, this);

        _boardClearer = new(board);
    }

    public async UniTask Execute(IItem item)
    {
        if (item is IBoostItem boostItem)
        {
            switch (boostItem.GetBoostType())
            {
                case BoostTypes.None:
                    break;
                case BoostTypes.Bomb:
                    await _bomb.Execute(boostItem);
                    break;
                case BoostTypes.Horizontal:
                    await _horisontal.Execute(boostItem);
                    break;
                case BoostTypes.Vertical:
                    await _vertical.Execute(boostItem);
                    break;
                case BoostTypes.Rainbow:
                    await _rainbow.Execute(boostItem);
                    break;
                case BoostTypes.Rocket:
                    await _rocket.Execute(boostItem);
                    break;
                default:
                    break;
            }
        }

        _boardClearer.ClearBordFromDeadItems();
    }

    public async UniTask Execute(IItem item, ToolTypes toolTypes)
    {
        switch (toolTypes)
        {
            case ToolTypes.Remover:
                break;
            case ToolTypes.Swaper:
                break;
            case ToolTypes.XLiner:
                await _xlines.Execute(item);
                break;
            default:
                break;
        }
    }



}
