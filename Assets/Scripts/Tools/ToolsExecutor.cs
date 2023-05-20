using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class ToolsExecutor : MonoBehaviour
{
    [SerializeField] private ToolSelector _toolSelector;
    [SerializeField] private ToolDeselector _toolDeselector;
    [SerializeField] private TouchInput _touchInput;

    private CellSelector _cellSelector;
    private RemoverTool _removerTool;
    private SwaperTool _swaperTool;
    private XLinerTool xLinerTool;

    private ITool _selectedTool;

    [Inject]
    private BoardSolver boardSolver;
    [Inject]
    private ItemSwaper _itemSwaper;
    [Inject]
    private IBoostExicuter _boostExicuter;

    private bool _isInputActive = true;

    public event UnityAction ToolExecuteStarted;

    public bool IsInputActive
    {
        set
        {
            _cellSelector.Forget();
            _isInputActive = value;
        }
    }

    private void Awake()
    {
        _cellSelector = new(_touchInput);

        _removerTool = new(_cellSelector, boardSolver);
        _swaperTool = new(_cellSelector, boardSolver, _itemSwaper);
        xLinerTool = new(_cellSelector, boardSolver, _boostExicuter);
    }

    private void OnEnable()
    {
        _toolSelector.ToolSelected += OnToolSelected;
    }

    private void OnDisable()
    {
        _toolSelector.ToolSelected -= OnToolSelected;
    }

    private void OnDestroy()
    {
        _removerTool.Dispose();
    }

    public async UniTask ExecuteTool(ToolTypes type)
    {
        switch (type)
        {
            case ToolTypes.Remover:
                _selectedTool = _removerTool;
                break;
            case ToolTypes.Swaper:
                _selectedTool = _swaperTool;
                break;
            case ToolTypes.XLiner:
                _selectedTool = xLinerTool;
                break;
        }

        _selectedTool.ExecuteStarted += OnToolExecuteStarted;
        await _selectedTool.Execute();
    }

    private void OnToolExecuteStarted()
    {
        ToolExecuteStarted?.Invoke();
        Deselect();
    }

    public void Deselect()
    {
        _toolDeselector.Deselect();

        if (_selectedTool != null)
        {
            _selectedTool?.Forget();
            _selectedTool.ExecuteStarted -= OnToolExecuteStarted;
            _selectedTool = null;
        }

    }

    private void OnToolSelected(ToolTypes type)
    {
        if (_isInputActive == false)
        {
            return;
        }

        if (_selectedTool == null)
        {
            ExecuteTool(type);
        }
    }
}
