using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;


public class BoostInjector : MonoBehaviour
{
    [SerializeField] private BoostSelector _boostSelector;
    [SerializeField] private TouchInput _touchInput;
    [SerializeField] private Matcher _matcher;

    [Inject] private IBoardFiller _boardFiller;

    private BoostTypes _selectedType;
    private CellRayCaster _cellRayCaster = new();

    private void OnEnable()
    {
        _boostSelector.BoostSelected += OnBoostChanged;
        _touchInput.PointerDown += OnPointerDown;
    }

    private void OnDisable()
    {
        _boostSelector.BoostSelected -= OnBoostChanged;
    }

    private void OnBoostChanged(BoostTypes type)
    {
        _selectedType = type;
    }

    private void OnPointerDown(Vector3 position)
    {
        if (_selectedType != BoostTypes.None &&
            _cellRayCaster.TryGetCell(position, out var cell))
        {
            cell.Item.View.Hide();
            InjectBoost(cell, _selectedType);
        }
    }

    private void InjectBoost(ICell cell, BoostTypes type)
    {
        _matcher.IsInputActive = false;

        _boardFiller.FillBoostInCell(type, cell);

        _boostSelector.SetSelectedBoost();
        _matcher.IsInputActive = true;
    }


}
