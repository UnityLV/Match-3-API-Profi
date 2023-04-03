using Assets.Scripts.Code.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePanel : MonoBehaviour
{
    private const string Name = "Name";
    [SerializeField] private TMP_InputField inputText;
    [SerializeField] private TMP_Text nameTextInProfile;
    [SerializeField] private CanvasGroup interNameCanvasGroup;
    private CanvasController _canvasController;
    private string _name;
    private void Start()
    {
        _canvasController = CanvasController.instance;
        if (PlayerPrefs.HasKey(Name))
        {
            _name = PlayerPrefs.GetString(Name);
            nameTextInProfile.text = PlayerPrefs.GetString(Name);
            inputText.text = _name;
        }
    }
    public void SetName(string value)
    {
        _name = value;
        nameTextInProfile.text = _name;
        PlayerPrefs.SetString(Name, _name);
    }
    public void OpenNamePanel()
    {
        _canvasController.Setter.TurnOnCanvasGroup(interNameCanvasGroup);
        inputText.ActivateInputField();
    }
}
