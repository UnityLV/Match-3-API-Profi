using UnityEngine;

public class UITool : MonoBehaviour
{
    public bool IsToolSelected;
    [SerializeField] private ToolSelector toolSelector;
    [SerializeField] private int toolAmount = 3;


    public void TryToSelectTool(int type)
    {
        toolSelector.SelectTool(type);
    }
}
