using Assets.Scripts.Code.Bank;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ToolBank : BankDefault
{
    [SerializeField] private Image amoutImage;
    [SerializeField] private int toolId;
    private CustomFont _customFont;


    private void Start()
    {
        _customFont = CustomFont.Instance;
        if (PlayerPrefs.HasKey($"Tools {toolId}"))
        {
            Add(PlayerPrefs.GetInt($"Tools {toolId}"));
        }
        else
        {
            Add(3);
        }
    }
    public override void Add(int value)
    {
        base.Add(value);
        PlayerPrefs.SetInt($"Tools {toolId}", Value);
        amoutImage.sprite = _customFont.CreateCustomFontFromNumber(Value)[0]; 
    }

    public override bool TryRemove(int value)
    {
        if (base.TryRemove(value))
        {
            PlayerPrefs.SetInt($"Tools {toolId}", Value);
            amoutImage.sprite = _customFont.CreateCustomFontFromNumber(Value)[0];
            return true;
        }
        return false;
    }
}
