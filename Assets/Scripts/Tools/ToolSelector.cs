using UnityEngine;
using UnityEngine.Events;

public class ToolSelector : MonoBehaviour
{
    public event UnityAction<ToolTypes> ToolSelected;

    public void SelectTool(int type)
    {
        ToolSelected?.Invoke((ToolTypes)type);
    }
}
