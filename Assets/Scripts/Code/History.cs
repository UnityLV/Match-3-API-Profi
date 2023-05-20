using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class History : MonoBehaviour
{
    public static History instance = null;

    [SerializeField] private NovelController novelController;
    [SerializeField] private LoadLevel loadLevel;
    [SerializeField] private GameObject defaultGroup;
    [SerializeField] private TMP_Text levelIdText;
    private const string DialogID = "DialogID";
    private const string LevelID = "LevelID";
    private int _dialogID;
    private int _levelID;
    private CanvasController _canvasController;
    private int tutorialNumber = 1;
    public int CurrentLevelID =>_levelID;
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
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        if (PlayerPrefs.HasKey(DialogID))
            _dialogID = PlayerPrefs.GetInt(DialogID);
        if (PlayerPrefs.HasKey(LevelID))
            _levelID = PlayerPrefs.GetInt(LevelID);
        novelController.Initialize(OnDialogEnd);
        loadLevel.Initialize(OnSceneLoaded);
        _canvasController = CanvasController.instance;
        levelIdText.text = (_levelID + 1).ToString();
    }
    public void OnStartGame()
    {
        if (_dialogID == 0)
        {
            novelController.StartDialog();
        }
    }
    public void StartLevel(int id)
    {
        if (_canvasController.HeartsBank.Value<=0)
        {
            _canvasController.HeartsMenuActivate();
            return;
        }
        loadLevel.LoudScene(id);
        LevelSelector.SelectedLevel = _levelID;
    }

    private void OnDialogEnd(int dialogId)
    {
        //_dialogID = dialogId;
        //if (dialogId == 1)
        //{
        //    StartLevel(1);
        //    _levelID = 2;
        //}
    }
    private void OnSceneLoaded(int indexScene)
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        DefaultMenuSwitcher(buildIndex);
        tutorialNumber = (_levelID+1)* 2;
        if (_levelID+1 ==10)
        {
            return;
        }
        if (buildIndex == 1)
        {
            novelController.StartDialog(tutorialNumber, true);
        }
    }
    private void DefaultMenuSwitcher(int buildIndex)
    {
        if (buildIndex > 0)
        {
            _canvasController.AllDefaultPanelDeactivate();
            _canvasController.MenuDeactivate();
        }
        else
        {
            _canvasController.AllDefaultPanelActivate();
            levelIdText.text = (_levelID+1).ToString();
        }
    }
    public void LevelComplited()
    {
        _levelID++;
        PlayerPrefs.SetInt(LevelID, _levelID);
    }
}
