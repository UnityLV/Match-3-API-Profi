using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuWin : MonoBehaviour
{
    public bool IsActive;
    [SerializeField] private Image[] moneyImages;

    private CustomFont _customFont;

    private void Start()
    {
        _customFont = CustomFont.Instance;
    }
    private void Update()
    {
        
        if (Input.touchCount > 0&&IsActive)
        {
            LoadLevel.instance.LoudScene(0);
            IsActive = false;
        }
    }
    public void Activate(AwardsScriptableObject awardsScriptableObject)
    {
        List<Sprite> money = _customFont.CreateCustomFontFromNumber(awardsScriptableObject.Money);
        moneyImages[0].sprite = money[1];
        moneyImages[1].sprite = money[0];
    }
}
