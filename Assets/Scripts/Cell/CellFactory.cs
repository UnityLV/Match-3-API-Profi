using Common.Extensions;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(CellFactory), menuName = "ScriptableObjects/" + nameof(CellFactory), order = 1)]
public class CellFactory : TextureMapFactory<GridPosition, ICell>
{
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private IntBinds _idBinds;
    private ConfigPool<int> _idPool;

    public int With { get; private set; }
    public int Height { get; private set; }

    public override void Init(IStateObserver StateObserver)
    {

    }

    public override void SetMap(Texture2D cellMap)
    {
        With = cellMap.width;
        Height = cellMap.height;

        _idPool = new(_idBinds);
        _idPool.Init(cellMap);
    }

    public override ICell Get(GridPosition position)
    {
        var cell = _cellPrefab.CreateNew<ICell>();

        InitCell(position, cell);

        return cell;
    }

    private void InitCell(GridPosition position, ICell cell)
    {
        int id = GetId(position);

        cell.Init(position);

        cell.Type = (CellTypes)id;
    }


    private int GetId(GridPosition position)
    {
        int id = default;

        bool isIdExistInPool = _idPool.Values.Length > position.ColumnIndex &&
            _idPool.Values[position.ColumnIndex].TryDequeue(out id);

        if (isIdExistInPool)
            return id;

        return default;
    }

}
