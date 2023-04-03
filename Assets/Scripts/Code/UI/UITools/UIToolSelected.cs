using UnityEngine;
using UnityEngine.UI;

public class UIToolSelected : MonoBehaviour
{
    [SerializeField] private Image UIToolImage;
    [SerializeField] private GameObject crose;
    [SerializeField] private GameObject item;
    [SerializeField] private ToolDeselector toolDeselector;
    [SerializeField] private ToolsExecutor toolExecutor;
    [SerializeField] private UITool UITool;
    [SerializeField] private ToolBank toolBank;

    [SerializeField] private Color color = new Color(1, 0.82f, 0.81f, 1);

    private bool isSelected;
    private void Start()
    {
        toolDeselector.ToolDeselected += OnToolDeselect;
        toolExecutor.ToolExecuteStarted += OnToolExecute;
    }
    public void OnToolClicked(int type)
    {
        if (UITool.IsToolSelected)
            return;
        if (toolBank.Value < 1)
            return;
        UITool.IsToolSelected = true;
        UIToolImage.color = color;
        crose.SetActive(true);
        item.SetActive(false);
        UITool.TryToSelectTool(type);
        isSelected = true;
        Debug.Log("Tool Selected");
    }
    public void OnToolExecute()
    {
        if (!isSelected)
            return;
        if (toolBank.TryRemove(1))
        {
            Debug.Log("Tool Removed");
        }
    }
    public void OnToolDeselect()
    {
        UITool.IsToolSelected = false;
        UIToolImage.color = Color.white;
        crose.SetActive(false);
        item.SetActive(true);
        isSelected = false;
    }
}
