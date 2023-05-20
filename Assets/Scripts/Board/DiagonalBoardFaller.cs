using Cysharp.Threading.Tasks;

public class DiagonalBoardFaller : IBoardFaller
{
    private Board _board;

    private int _rows;
    private int _columns;

    private int _fallRowDelay = 30;

    public DiagonalBoardFaller(Board board)
    {
        _board = board;
        _rows = _board.Rows;
        _columns = _board.Columns;
    }

    public async UniTask FallBoard()
    {
        for (int rowIndex = 0; rowIndex < _rows; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < _columns; columnIndex++)
            {
                
            }
            await UniTask.Delay(_fallRowDelay);
        }
    }

}
