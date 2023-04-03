using UnityEngine;
using UnityEngine.UI;

public class CharacterImage : MonoBehaviour
{
    [SerializeField] private string emotion;
    [SerializeField] private Image image;
    public string Emotion => emotion;
    public void Changeblackout(bool isBlack)
    {
        if (isBlack)
        {
            image.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            image.color = new Color(1, 1, 1);

        }
    }
}
