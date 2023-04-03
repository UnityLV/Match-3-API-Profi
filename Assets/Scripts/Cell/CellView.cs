using UnityEngine;

public class CellView : MonoBehaviour, ICellView
{
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    public void SetWorldPosition(Vector3 positon)
    {
        transform.position = positon;
    }
}
