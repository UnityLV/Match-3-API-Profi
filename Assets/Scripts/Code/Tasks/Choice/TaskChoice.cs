using Assets.Scripts.Code.Bank;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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
