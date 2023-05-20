using System.Collections.Generic;
using UnityEngine;

public class MidleCellFinder
{
    public Vector3 Find(ICell[,] cells)
    {
        List<Vector3> itemPositions = new List<Vector3>();
        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                ICell cell = cells[i, j];
                if (cell.CanContainsItem)
                {
                    itemPositions.Add(cell.View.GetWorldPosition());
                }
            }
        }

        if (itemPositions.Count == 0)
        {
            // Handle case when no cells can contain item
            return Vector3.zero;
        }

        // Find bounding box of item positions
        Vector3 min = itemPositions[0];
        Vector3 max = itemPositions[0];
        foreach (Vector3 position in itemPositions)
        {
            min = Vector3.Min(min, position);
            max = Vector3.Max(max, position);
        }

        // Calculate center of bounding box
        Vector3 center = (min + max) / 2f;

        return center;

    }
}
