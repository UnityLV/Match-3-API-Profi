using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public delegate void DialogEnd(int dialogID);
public class VisualNovel : MonoBehaviour, IInit<DialogEnd>
{
    private const string DialogID = "DialogID";

    [SerializeField] private FileService fileService;
    [SerializeField] private TMP_Text nameLeft;
    [SerializeField] private TMP_Text nameRight;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private RectTransform backgroundLeft;
    [SerializeField] private RectTransform backgroundRight;
    [SerializeField] private Character[] characters;
    [SerializeField] private float duration = 1;
    [SerializeField] private float characterDuration = 0.2f;
    [Header("Tutorial")]
    [SerializeField] private GameObject tutorialBackground;
    [SerializeField] private TMP_Text tutorialText;


    private event DialogEnd _dialogEnd;
    private bool _isNovelActive;
    private Dictionary<string, Character> AllCharacters;
    private int _dialogID = 0;
    private int _messageID = 0;
    private Character _curentCharacter;
    private Character _secondCharacter;
    private RectTransform rectTransform;
    private int _currentDialog;
    private bool _isTutor;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        AllCharacters = new Dictionary<string, Character>();
        for (int i = 0; i < characters.Length; i++)
        {
            Character item = characters[i];
            AllCharacters.Add(item.Name, item);
        }
        characters = null;
        if (PlayerPrefs.HasKey(DialogID))
            _dialogID = PlayerPrefs.GetInt(DialogID);
    }
    private void Update()
    {
        if (_isNovelActive)
        {
            ShowDialog(_currentDialog);
        }
    }
    public void StartNovel(int dialogID, bool isTutor)
    {
        if (dialogID == -1)
            dialogID = _dialogID;
        _currentDialog = dialogID;
        _isNovelActive = true;
        _isTutor = isTutor;
    }
    private void EndNovel()
    {
        _isNovelActive = false;
        PlayerPrefs.SetInt(DialogID, _dialogID + 1);
        _dialogEnd?.Invoke(_dialogID);
    }
    public void ShowMesage(int messageID, int dialogID)
    {
        string message = "";
        string imageType = "";
        string name = GetMessageWhisNameAndImage(out message, out imageType, fileService.Histrory[dialogID][messageID]);
        if (_isTutor)
        {
            ShowTutorialMessage(message);
            return;
        }
        nameLeft.text = name;
        nameRight.text = name;
        messageText.text = message;
        Character newCharacter = AllCharacters[name];
        if (_curentCharacter == null)
        {
            _curentCharacter = newCharacter;
        }
        if (newCharacter != _curentCharacter)
        {
            _curentCharacter.CurentCharacterImage.Changeblackout(true);
            newCharacter.SwitchCurentCharacterImage(imageType);
            _secondCharacter = _curentCharacter;
            _curentCharacter = newCharacter;
            _curentCharacter.transform.DOScale(1f, characterDuration);
            _curentCharacter.CurentCharacterImage.Changeblackout(false);
        }
        else if (_curentCharacter.CurentCharacterImage != newCharacter.AllCharacterImages[imageType])
        {
            newCharacter.SwitchCurentCharacterImage(imageType);
            _curentCharacter.CurentCharacterImage.Changeblackout(false);
            _curentCharacter.transform.DOScale(1f, characterDuration);
        }
        AnimateBackground();
    }
    private void ShowTutorialMessage(string message)
    {
        backgroundLeft.gameObject.SetActive(false);
        backgroundRight.gameObject.SetActive(false);
        messageText.gameObject.SetActive(false);
        tutorialBackground.SetActive(true);
        tutorialText.text = message;
    }
    private void AnimateBackground()
    {
        if (!backgroundLeft.gameObject.activeInHierarchy && !backgroundRight.gameObject.activeInHierarchy)
        {
            rectTransform.DOAnchorPosY(-200, duration).From();
        }
        ShowBacground();
    }
    private void ShowBacground()
    {
        if (_isTutor)
        {
            backgroundLeft.gameObject.SetActive(false);
            backgroundRight.gameObject.SetActive(false);
            return;
        }
        if (!_curentCharacter.isLeftSide)
        {
            backgroundLeft.gameObject.SetActive(false);
            backgroundRight.gameObject.SetActive(true);
        }
        else
        {
            backgroundLeft.gameObject.SetActive(true);
            backgroundRight.gameObject.SetActive(false);
        }
    }

    public void ShowDialog(int dialogID)
    {
        bool isClicked = false;
        if (Input.touchCount>0)
        {
            isClicked = Input.GetTouch(0).phase == TouchPhase.Began;
        }
        if (_messageID == 0)
        {
            isClicked = true;
        }
        //Debug.Log(isClicked);
        if (isClicked)
        {
            if (_messageID >= fileService.Histrory[dialogID].Count)
            {
                EndNovel();
                backgroundLeft.gameObject.SetActive(false);
                backgroundRight.gameObject.SetActive(false);
                tutorialBackground.SetActive(false);
                _dialogID++;
                _messageID = 0;
                _secondCharacter?.SwitchOff();
                _secondCharacter = null;
                _curentCharacter?.SwitchOff();
                _curentCharacter = null;
                messageText.gameObject.SetActive(true);
                return;
            }
            ShowMesage(_messageID, dialogID);
            _messageID++;
            isClicked = false;
        }
    }
    private string GetMessageWhisNameAndImage(out string messageWithoutName, out string imageType, string fullMessage)
    {
        string name = "";
        messageWithoutName = "";
        imageType = "";
        bool isImage = false;
        bool isName = true;
        for (int i = 0; i < fullMessage.Length; i++)
        {
            bool isEndName = fullMessage[i] == ':' && fullMessage[i + 1] == ':';
            if (isEndName && isImage)
            {
                isImage = false;
                i += 2;
            }
            else if (isEndName)
            {
                isName = false;
                isImage = true;
                i += 2;
            }
            if (isImage)
            {
                imageType += fullMessage[i];
                continue;
            }
            if (isName)
            {
                name += fullMessage[i];
                continue;
            }
            messageWithoutName += fullMessage[i];
        }
        name = name.Substring(0, name.Length - 1);
        return name;
    }

    public void Initialize(DialogEnd @delegate)
    {
        _dialogEnd += @delegate;
    }

    public void Deinitialize(DialogEnd @delegate)
    {
        _dialogEnd -= @delegate;
    }
}
