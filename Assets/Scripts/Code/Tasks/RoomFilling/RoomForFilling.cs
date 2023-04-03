using Assets.Scripts.Code.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomForFilling : MonoBehaviour
{
    private const string RoomForFillingIndex = "RoomForFilling";
    [SerializeField] private RoomItems[] roomItems;
    [SerializeField] private RoomItems[] roomSecondItems;
    [SerializeField] private CompleteTask completeTask; 
    [SerializeField] private NovelController novelController;
    private bool _isPrepared;
    private int _fillingIndex;
    private void Start()
    {
        if (PlayerPrefs.HasKey(RoomForFillingIndex))
        {
            _fillingIndex = PlayerPrefs.GetInt(RoomForFillingIndex);
        }
    }
    public void ItemsSwitch(UnityAction onSwiched)
    {
        roomItems[_fillingIndex].SwitchFade(onSwiched);
        _fillingIndex++;
    }
    public void ItemsSwitchNext(UnityAction onSwiched)
    {
        roomSecondItems[0].SwitchFade(onSwiched);
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetInt(RoomForFillingIndex, _fillingIndex);
    }
}
