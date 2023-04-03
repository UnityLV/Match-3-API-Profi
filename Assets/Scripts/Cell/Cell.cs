using System;
using UnityEngine;

public class Cell : MonoBehaviour, ICell
{
    private CellTypes _type;

    public IItem Item { get; set; }

    public CellTypes Type
    {
        get => _type;
        set
        {
            SetType(value);
            _type = value;
        }
    }

    public GridPosition GridPosition { get; private set; }

    public bool HasItem => Item != null;

    public bool CanContainsItem { get; private set; }

    public bool CanSetItem => CanContainsItem && HasItem == false;

    public ICellView View { get; private set; }

    public bool IsBlocked { get; private set; }

    private void Awake()
    {
        View = GetComponent<ICellView>();
    }

    public void Init(GridPosition position)
    {
        name = position.ToString();
        GridPosition = position;

        View.SetWorldPosition(new Vector3(position.ColumnIndex, position.RowIndex));
    }

    public void Clear()
    {
        Item = null;
    }


    public void SetType(CellTypes type)
    {
        switch (type)
        {
            case CellTypes.Empty:
                SetEmptyState();
                break;
            case CellTypes.Avalable:
                SetAvalableState();
                break;
            case CellTypes.Origin:
                SetOriginState();
                break;            
        }
    }

    private void SetOriginState()
    {
        View.Deactivate();
        IsBlocked = true;
        CanContainsItem = false;
    }

    private void SetEmptyState()
    {
        CanContainsItem = false;
        IsBlocked = true;
        View.Deactivate();
    }

    private void SetAvalableState()
    {
        IsBlocked = false;
        CanContainsItem = true;
    }


}


