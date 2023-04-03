using UnityEngine;

public class CellRayCaster
{
    public bool TryGetCell(Vector3 point, out ICell cell)
    {
        var hits = Physics2D.RaycastAll(point, Vector3.forward);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.transform.parent.TryGetComponent(out cell))
            {
                return true;
            }
        }
        cell = default;
        return false;
    }
}
