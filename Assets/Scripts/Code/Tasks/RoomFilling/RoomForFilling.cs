using Assets.Scripts.Code.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomForFilling : MonoBehaviour
{
    private const string RoomForFillingIndex = "RoomForFilling";

    public static RoomForFilling instance = null;
    [SerializeField] private CompleteTask completeTask; 
    [SerializeField] private NovelController novelController;
    private bool _isPrepared;
    private int _fillingIndex;

    private static RoomService roomService;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey(RoomForFillingIndex))
        {
            _fillingIndex = PlayerPrefs.GetInt(RoomForFillingIndex);
        }
    }
    public void SetRooms(RoomService roomService)
    {
        RoomForFilling.roomService = roomService;
    }
    public void ItemsSwitch(UnityAction onSwiched)
    {
        roomService.roomItems[_fillingIndex].SwitchFade(onSwiched);
        _fillingIndex++;
    }
    public void ItemsSwitchNext(UnityAction onSwiched)
    {
        roomService.roomSecondItems[0].SwitchFade(onSwiched);
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetInt(RoomForFillingIndex, _fillingIndex);
    }
}
