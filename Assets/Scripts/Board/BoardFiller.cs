using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardFiller : IBoardFiller
{
    private RegularBoardFiller _regularBoardFiller;
    private BoostBoardFiller _boostBoardFiller;
    private ObsticleBoardFiller _obsticleBoardFiller;

    private Board _board;
    private List<ICell> _origins;

    public BoardFiller(ConfigFactory<int, IItem> itemFactory,
        ConfigFactory<BoostTypes, IBoostItem> boostFactory,
        IEnumerable<ConfigFactory<int, IItem>> obsticleFactoryes, Board board)
    {
        _regularBoardFiller = new(itemFactory);
        _boostBoardFiller = new(boostFactory);
        _obsticleBoardFiller = new(obsticleFactoryes);
        _board = board;
        InitialFillBoard();
    }

    public int Rows => _board.Rows;
    public int Collumns => _board.Columns;

    public void InitialFillBoard()
    {
        _origins = GetOrigins();

        for (int rowIndex = 0; rowIndex < Rows; rowIndex++)
        {
            for (int collumnIndex = 0; collumnIndex < Collumns; collumnIndex++)
            {
                var cell = _board[rowIndex, collumnIndex];

                if (_obsticleBoardFiller.TryFill(cell))
                {
                    continue;
                }

                _regularBoardFiller.TryFillCellRegularItem(cell, cell.View.GetWorldPosition());

            }
        }
    }

    public async UniTask FillBoard()
    {
        _origins ??= GetOrigins();

        await FillFromOrigins();

    }

    private List<ICell> GetOrigins()
    {
        List<ICell> origins = new();
        for (int rowIndex = 0; rowIndex < Rows; rowIndex++)
        {
            for (int collumnIndex = 0; collumnIndex < Collumns; collumnIndex++)
            {
                var cell = _board[rowIndex, collumnIndex];
                TryAddToOrigins(origins, cell);
            }
        }

        return origins;
    }

    private void TryAddToOrigins(List<ICell> origins, ICell cell)
    {
        bool isOrigin = cell.Type == CellTypes.Origin;

        if (isOrigin)
            origins.Add(cell);
    }


    public void FillBoostInCell(BoostTypes type, ICell cell)
    {
        _boostBoardFiller.FillBoostInCell(type, cell);
    }

    private async UniTask FillFromOrigins()
    {
        await UniTask.WhenAll(_origins.Select(cell => FillCollumnFromOriginCell(cell)).ToArray());
    }

    private async UniTask FillCollumnFromOriginCell(ICell origin)
    {
        List<CellFillData> cellToFillData = GetCellsFillData(origin);

        foreach (var cellFillData in cellToFillData)
        {
            await _regularBoardFiller.TryFillCellRegularItem(cellFillData.Cell, cellFillData.Path);
        }

    }

    private List<CellFillData> GetCellsFillData(ICell origin)
    {
        List<CellFillData> cellToFill = new();
        List<Vector3> path = new();

        path.Add(origin.View.GetWorldPosition());

        for (int rowIndex = origin.GridPosition.RowIndex; rowIndex >= 0; rowIndex--)
        {
            bool isFillingOrigin = rowIndex == origin.GridPosition.RowIndex;

            if (isFillingOrigin)
            {
                continue;
            }

            var fillingCell = _board[rowIndex, origin.GridPosition.ColumnIndex];
            bool isNeedToFill = fillingCell.HasItem == false && fillingCell.CanContainsItem;

            if (isNeedToFill)
            {
                path.Add(fillingCell.View.GetWorldPosition());
                cellToFill.Add(new(fillingCell, path.ToArray()));
            }
            else
            {
                break;
            }

        }

        cellToFill.Reverse();
        return cellToFill;
    }





}
