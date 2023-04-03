using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public delegate void SwitchProfileCell(CellProfile cell);
public class CellProfile : MonoBehaviour, IInit<SwitchProfileCell>
{
    public bool IsActive;
    [SerializeField] private GameObject checkMark;
    [SerializeField] private Button button;
    private event SwitchProfileCell _switchProfileCell;
    private void Start()
    {
        button.onClick.AddListener(Activate);
    }
    public void Activate()
    {
        IsActive = true;
        checkMark.SetActive(true);
        _switchProfileCell?.Invoke(this);
    }
    public void Deactivate()
    {
        IsActive=false;
        checkMark.SetActive(false);
    }

    public void Deinitialize(SwitchProfileCell @delegate)
    {
        _switchProfileCell -= @delegate;
    }

    public void Initialize(SwitchProfileCell @delegate)
    {
        _switchProfileCell += @delegate;
    }
    private void OnDestroy()
    {
        button.onClick.RemoveListener(Activate);
    }
}
