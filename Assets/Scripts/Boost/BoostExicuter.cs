using Cysharp.Threading.Tasks;


public interface IBoostExicuter
{
    UniTask Execute(IBoostItem boostItem);
    UniTask Execute(IItem item, ToolTypes toolTypes);
}


public class BoostExicuter : IBoostExicuter
{
    private IBoostAction _horisontal;
    private IBoostAction _vertical;
    private IBoostAction _bomb;
    private IBoostAction _rainbow;
    private IBoostAction _rocket;
    private IBoostAction _xlines;

    private BoardClearer _boardClearer;

    public BoostExicuter(Board board, GoalCellOnBoardFinder goalItemFinder)
    {
        _horisontal = new HorizontalLineRemover(board,  this);
        _vertical = new VerticalLineRemover(board,  this);
        _bomb = new BombBoostAction(board,  this);
        _rainbow = new RainbowBoostAction(board,  this);
        _rocket = new RocketBoostAction(board, goalItemFinder,  this);
        _xlines = new XLineRemover(board,  this);

        _boardClearer = new(board);
    }

    public async UniTask Execute(IBoostItem boostItem)
    {
        if (boostItem.IsUsed == true)
        {
            return;
        }

        boostItem.IsUsed = true;

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
