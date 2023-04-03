using UnityEngine;

public class CellFillData
{
    public CellFillData(ICell cell, Vector3[] path)
    {
        Cell = cell;
        Path = path;
    }

    public ICell Cell { get; }
    public Vector3[] Path { get; }
}
