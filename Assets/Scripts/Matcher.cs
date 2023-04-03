using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Matcher : MonoBehaviour
{
    [SerializeField] private ItemSwaper _itemSwaper;
    [SerializeField] private TouchInput _input;

    [Inject] private BoardSolver _boardSolver;

    private CellSelector _cellSelector;
    private bool _isInputActive = true;
    public event UnityAction TouchAvailable;

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
        _cellSelector = new(_input);
    }

    private void OnEnable()
    {
        _cellSelector.SelectSecondCell += OnSelectSecondCell;
    }

    private void OnDisable()
    {
        _cellSelector.SelectSecondCell -= OnSelectSecondCell;
    }

    private async void OnSelectSecondCell(ICell cell1, ICell cell2)
    {
        if (_isInputActive == false)
        {
            return;
        }
        _cellSelector.BlokTouch();
        await HandleSelectedCells(cell1, cell2);
        TouchAvailable?.Invoke();
        _cellSelector.Reset();
    }

    private async UniTask HandleSelectedCells(ICell firstCell, ICell secondCell)
    {
        bool isSwap = _itemSwaper.IsCanSwap(firstCell, secondCell);

        if (isSwap)
        {
            await _itemSwaper.SwapItems(firstCell, secondCell);
            await _boardSolver.SolveSwap(firstCell, secondCell);
        }
    }
}