public class Workers
{

    public Workers(SolveSlotsDetecor solveSlotsDetecor, Board board, IBoardFiller boardFiller)
    {
        SolveSlotsDetecor = solveSlotsDetecor;
        BoardFaller = new DownBoardFaller(board);
        BoardFiller = boardFiller;
        PositionMemory = new(board);
        ItemStateMover = new();
        ObstilcleSolver = new(board);
        BoardClearer = new(board);
    }

    public SolveSlotsDetecor SolveSlotsDetecor { get; }
    public IBoardFaller BoardFaller { get; }
    public IBoardFiller BoardFiller { get; }
    public PositionMemory PositionMemory { get; }
    public ItemNextStateMover ItemStateMover { get; }
    public ObstilcleSolver ObstilcleSolver { get; }
    public BoardClearer BoardClearer { get; }

}




