using UnityEngine;
using UnityEngine.Events;

public class ToolDeselector : MonoBehaviour
{
    public event UnityAction ToolDeselected;

    public void Deselect()
    {
        ToolDeselected?.Invoke();
    }
}
