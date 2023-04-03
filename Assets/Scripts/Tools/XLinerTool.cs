using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class XLinerTool : SingleCellTool
{

    private BoostExicuter _boostExicuter;

    public XLinerTool(CellSelector cellSelector, BoardSolver boardSolver, BoostExicuter boostExicuter) : base(cellSelector, boardSolver)
    {
        _boostExicuter = boostExicuter;
    }

    protected override async UniTask ToolAction(ICell cell)
    {
        await _boostExicuter.Execute(cell.Item, ToolTypes.XLiner);
    }

}