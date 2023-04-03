using Assets.Scripts.Code.Bank;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeartsAmount : MonoBehaviour
{
    [SerializeField] private Hearts _hearts;
    [SerializeField] private TMP_Text _textHearts;

    private void Start()
    {
        _hearts.AddObserver(OnHeatsValueChanged);
    }
    private void OnHeatsValueChanged(int oldValue, int value)
    {
        _textHearts.text = value.ToString();
    }
    private void OnDestroy()
    {
        _hearts.RemoveObserver(OnHeatsValueChanged);
    }
}
