using UnityEngine;
using UnityEngine.UI;

public class CustomOject : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private GameObject[] objectsForChooce;
    [SerializeField] private ChoiceScrptableObject choiceScrptableObjects;
    public int Index => index;

    private int selectedIndex;
    public void StartChoise(Image[] choiceImages)
    {
        for (int i = 0; i < choiceImages.Length; i++)
        {
            choiceImages[i].sprite = choiceScrptableObjects.Sprites[i];
        }
    }
    public void TryChoice(int index)
    {
        for (int i = 0; i < objectsForChooce.Length; i++)
        {
            if (i == index)
            {
                objectsForChooce[i].SetActive(true);
                continue;
            }
            objectsForChooce[i].SetActive(false);
        }
    }
    public void ConfirmSelection(int index = -1)
    {
        if (index == -1)
            index = selectedIndex;
        TryChoice(index);
    }
}
