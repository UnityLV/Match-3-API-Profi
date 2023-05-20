using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayGoal : MonoBehaviour
{
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private Image goalImage;
    public TMP_Text AmountText => amountText;
    public Image GoalImage => goalImage;
}
