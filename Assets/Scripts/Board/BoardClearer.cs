public class BoardClearer
{
    private Board _board;

    public BoardClearer(Board board)
    {
        _board = board;
    }

    public void ClearCell(ICell cell)
    {
        cell.Clear();
    }

    public void ClearBordFromDeadItems()
    {
        foreach (ICell cell in _board)
        {
            if (cell.Item == null || cell.Item.State.IsAlive == false)
            {
                ClearCell(cell);
            }
        }
       
    }

}


