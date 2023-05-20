using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGoal : MonoBehaviour
{
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private Image goalImage;
    [SerializeField] private Image checkMark;
    public TMP_Text AmountText => amountText;
    public Image GoalImage => goalImage;
    public Image CheckMark => checkMark;
}
