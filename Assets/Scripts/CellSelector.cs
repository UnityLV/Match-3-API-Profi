using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Events;

public class CellSelector
{
    private TouchInput _input;
    private CellRayCaster _cellRayCaster = new();

    private ICell _selectedCell;
    private bool _isCanDrag;
    private bool _isCanTouch = true;

    public event UnityAction<ICell> SelectFirsCell;
    public event UnityAction<ICell, ICell> SelectSecondCell;

    
    public CellSelector(TouchInput input)
    {
        _input = input;
        _input.PointerDown += OnPointerDown;
        _input.PointerUp += OnPointerUp;
        _input.PointerDrag += OnPointerDrag;
    }

    public void Forget()
    {
        _selectedCell = null;
    }

    private void OnPointerDown(Vector3 position)
    {
        _isCanDrag = true;
        HandleTouch(position);
    }

    private void OnPointerDrag(Vector3 position)
    {
        if (_isCanDrag)
        {
            HandleTouch(position);
        }
    }

    private void OnPointerUp(Vector3 position)
    {
        _isCanDrag = false;
    }

    public void HandleTouch(Vector3 position)
    {
        if (_isCanTouch == false)
        {
            return;
        }        

        if (_cellRayCaster.TryGetCell(position, out ICell cell))
        {

            if (_selectedCell == null)
            {
                SelectFirstCell(cell);
                return;
            }

            bool isFoundNewCell = cell != _selectedCell;
            if (isFoundNewCell)
            {
                SelectSecondCell?.Invoke(_selectedCell, cell);
                _isCanDrag = false;
            }            
        }
    }

    private void SelectFirstCell(ICell cell)
    {
        _isCanTouch = true;
        _selectedCell = cell;
        SelectFirsCell?.Invoke(cell);
    }

    public void BlokDrag()
    {
        _isCanDrag = false;
    }

    public void BlokTouch()
    {

        _isCanTouch = false;
    }

    public void Reset()
    {
        _isCanTouch = true;        
        _selectedCell = null;
    }
}
