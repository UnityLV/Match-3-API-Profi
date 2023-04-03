using Cysharp.Threading.Tasks;

public interface IBoardFiller
{
    void InitialFillBoard();
    UniTask FillBoard();
    void FillBoostInCell(BoostTypes type, ICell cell);
}
