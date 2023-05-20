using UnityEngine;
using UnityEngine.UI;

public class TaskChoice : MonoBehaviour
{
    [SerializeField] private Image[] choiceImages;
    public Image[] ChoiceImages => choiceImages;

    [SerializeField] private CustomOject[] customOjects;
    [SerializeField] private CanvasController canvasController;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Increase increase;

    private CustomOject _currentObject;



    public void ActiveChoise(int taskIndex)
    {
        canvasController.MenuActivate(canvasGroup);
        increase.DoIncrease();
        _currentObject = customOjects[taskIndex];
        _currentObject.StartChoise(ChoiceImages);

    }
    public void TryChoice(int index = 0)
    {
        _currentObject.TryChoice(index);
    }
}
