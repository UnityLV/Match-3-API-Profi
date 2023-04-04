using Assets.Scripts.Code.Bank;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public delegate void BarRefreshed();
public class BarTask : BankDefault, IInit<BarRefreshed>
{
    
    [SerializeField] private Image barImage;
    [SerializeField] private TMP_Text barText;
    [SerializeField] private float duration;

    private const string precent = "precent";

    private event BarRefreshed _barRefreshed;
    private static bool isFirstTime = true;
    private void Start()
    {
        if (!isFirstTime)
        {
            return;
        }
        isFirstTime = false;
        if (PlayerPrefs.HasKey(precent))
        {
            Add(PlayerPrefs.GetInt(precent));
            RefreshBar();
        }
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetInt(precent, Value);
    }
    public override void Add(int addValue)
    {
        base.Add(addValue);
        RefreshBar();
    }
    public void RefreshBar()
    {
        barImage?.DOFillAmount((float)Value / 100, duration).OnComplete(() =>_barRefreshed?.Invoke());
        barText.text = Value.ToString() + "%";
        PlayerPrefs.SetInt(precent, Value);
    }

    public void Initialize(BarRefreshed @delegate)
    {
        _barRefreshed += @delegate;
    }

    public void Deinitialize(BarRefreshed @delegate)
    {
        _barRefreshed -= @delegate;
    }
}