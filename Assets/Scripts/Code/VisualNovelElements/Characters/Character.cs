using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string Name;
    public Dictionary<string, CharacterImage> AllCharacterImages;
    public CharacterImage CurentCharacterImage;
    public bool isLeftSide;

    [SerializeField] private CharacterImage[] _allCharacterImages;
    private void Awake()
    {
        AllCharacterImages = new Dictionary<string, CharacterImage>();
        for (int i = 0; i < _allCharacterImages.Length; i++) 
        {
            CharacterImage item = _allCharacterImages[i];
            AllCharacterImages.Add(item.Emotion, item);
        }
        _allCharacterImages = null;
    }
    public void SwitchOff()
    {
        CurentCharacterImage.gameObject.SetActive(false);
        CurentCharacterImage = null;
    }
    public void SwitchCurentCharacterImage(string imageType)
    {
        CurentCharacterImage?.gameObject.SetActive(false);
        CurentCharacterImage = AllCharacterImages[imageType];
        CurentCharacterImage.gameObject.SetActive(true);
    }
}
