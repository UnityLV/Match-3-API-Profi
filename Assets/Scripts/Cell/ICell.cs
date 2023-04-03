using UnityEngine;

public interface ICell
{
    bool HasItem { get; }    
    bool CanContainsItem { get; }
    bool CanSetItem { get; }
    bool IsBlocked { get; }


    ICellView View { get; }
    IItem Item { get; set; }
    CellTypes Type { get; set; }
    GridPosition GridPosition { get; }

    void Init(GridPosition position);

    void Clear();

}

public interface ICellView
{
    Vector3 GetWorldPosition();

    void SetWorldPosition(Vector3 positon);

    void Deactivate();
}
