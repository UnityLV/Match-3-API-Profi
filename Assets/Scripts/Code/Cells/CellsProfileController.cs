using System;
using UnityEngine;

public class CellsProfileController : MonoBehaviour
{
    private const string PlayerImage = "PlayerImage";
    [SerializeField] private CellProfile currentCell;
    [SerializeField] private CellProfile[] cells;
    private void Start()
    {
        foreach (CellProfile cell in cells)
        {
            cell.Initialize(SwitchProfileCell);
        }
        if (PlayerPrefs.HasKey(PlayerImage))
        {
            cells[PlayerPrefs.GetInt(PlayerImage)].Activate();
        }
    }
    public void SwitchProfileCell(CellProfile cell)
    {
        if (cell== currentCell)
        {
            return;
        }
        currentCell.Deactivate();
        currentCell = cell;
    }
    private void OnDestroy()
    {
        foreach (CellProfile cell in cells)
        {
            cell.Deinitialize(SwitchProfileCell);
        }
        PlayerPrefs.SetInt(PlayerImage, Array.IndexOf(cells, currentCell));
    }
}
