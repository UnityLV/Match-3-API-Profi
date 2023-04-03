using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

public class BoostSelector : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;

    public event UnityAction<BoostTypes> BoostSelected;

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int value)
    {
        switch (value)
        {
            case 0:
                BoostSelected.Invoke(BoostTypes.None);
                break;
            case 1:
                BoostSelected.Invoke(BoostTypes.Bomb);
                break;
            case 2:
                BoostSelected.Invoke(BoostTypes.Horizontal);
                break;
            case 3:
                BoostSelected.Invoke(BoostTypes.Vertical);
                break;
            case 4:
                BoostSelected.Invoke(BoostTypes.Rainbow);
                break;
            case 5:
                BoostSelected.Invoke(BoostTypes.Rocket);
                break;
                
            default:
                break;
        }
        
    }

    public void SetSelectedBoost(int index = default)
    {
        _dropdown.value = index;
    }
}
