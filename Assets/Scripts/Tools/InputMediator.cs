using UnityEngine;

public class InputMediator : MonoBehaviour
{
    [SerializeField] private Matcher _matcher;
    [SerializeField] private ToolsExecutor _toolsExecutor;

    [SerializeField] private ToolSelector _toolSelector;
    [SerializeField] private ToolDeselector _toolDeselector;

    private void OnEnable()
    {
        _toolSelector.ToolSelected += OnToolSelected;
        _toolDeselector.ToolDeselected += OnToolDeselected;
    }

    private void OnDisable()
    {
        _toolSelector.ToolSelected -= OnToolSelected;
        _toolDeselector.ToolDeselected -= OnToolDeselected;
    }

    private void OnToolDeselected()
    {
        _matcher.IsInputActive = true;
        _toolsExecutor.IsInputActive = false;
    }

    private void OnToolSelected(ToolTypes _)
    {
        _matcher.IsInputActive = false;
        _toolsExecutor.IsInputActive = true;
    }
}
