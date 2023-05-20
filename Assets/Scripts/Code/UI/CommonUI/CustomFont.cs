using System.Collections.Generic;
using UnityEngine;

public class CustomFont : MonoBehaviour
{
    public static CustomFont Instance;
    [SerializeField] private Sprite[] allNumbers;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public List<Sprite> CreateCustomFontFromNumber(int number)
    {
        List<Sprite> resultFontLine = new();
        int remainingNumber = number;
        do
        {
            resultFontLine.Add(allNumbers[remainingNumber % 10]);
            remainingNumber = remainingNumber / 10;
        }
        while (remainingNumber != 0);
        return resultFontLine;
    }
}
