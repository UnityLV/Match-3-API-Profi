using Assets.Scripts.Code.Bank;
using Assets.Scripts.Code.UI;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance = null;

    public CanvasSetter Setter;
    public Coins CoinsBank;
    public Stars StarsBank;
    public Hearts HeartsBank;
    [Header("Main Menus")]
    [SerializeField] private CanvasGroup heartsMenu;
    [SerializeField] private CanvasGroup taskMenu;
    [SerializeField] private CanvasGroup coinsMenu;
    [SerializeField] private CanvasGroup settingsMenu;
    [SerializeField] private CanvasGroup playMenu;
    [SerializeField] private CanvasGroup profileMenu;
    [SerializeField] private CanvasGroup winMenu;
    [SerializeField] private CanvasGroup smallSettings;
    [Space]
    [Header("Separate Canvas groups")]
    [SerializeField] private CanvasGroup[] allCanvasGroups;
    [Space]
    [Header("Animations")]
    [SerializeField] private RectTransform heartsRectTransform;
    [SerializeField] private RectTransform coinsRectTransform;
    [SerializeField] private RectTransform starsRectTransform;
    [SerializeField] private RectTransform settingsRectTransform;
    [SerializeField] private RectTransform taskRectTransform;
    [SerializeField] private RectTransform playRectTransform;
    [SerializeField] private RectTransform boxRectTransform;
    [SerializeField] private float duration = 0.5f;
    [Header("Delegates")]
    [SerializeField] private Decrease decreaseHearts;
    [SerializeField] private Decrease decreaseTask;
    [SerializeField] private Decrease decreaseSettings;
    [SerializeField] private Decrease decreasePlay;
    [SerializeField] private Decrease decreaseProfile;
    [Header("Other")]
    [SerializeField] private ParticleSystem heartsParticleSystem;
    private float _startCoinsValueX;
    private Dictionary<RectTransform, Vector2> allRectTransformsOnStart;
    private IInit<DecreaseComplete> _initDecreaseComplete;
    private UIMenuWin _menuWin;
    private bool _isActiveDefaultPanel;
    void Awake()
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
        Setter = new CanvasSetter();
        allRectTransformsOnStart = new Dictionary<RectTransform, Vector2>();
        SetPositions();

    }
    private void Start()
    {
        _menuWin = winMenu.GetComponent<UIMenuWin>();
        _startCoinsValueX = coinsRectTransform.anchoredPosition.x;
        InitializeDelegates();
    }

    private void SetPositions()
    {
        allRectTransformsOnStart.Add(heartsRectTransform, heartsRectTransform.anchoredPosition);
        allRectTransformsOnStart.Add(coinsRectTransform, coinsRectTransform.anchoredPosition);
        allRectTransformsOnStart.Add(starsRectTransform, starsRectTransform.anchoredPosition);
        allRectTransformsOnStart.Add(settingsRectTransform, settingsRectTransform.anchoredPosition);
        allRectTransformsOnStart.Add(taskRectTransform, taskRectTransform.anchoredPosition);
        allRectTransformsOnStart.Add(playRectTransform, playRectTransform.anchoredPosition);
        allRectTransformsOnStart.Add(boxRectTransform, boxRectTransform.anchoredPosition);
    }
    public void AllDefaultPanelActivate()
    {
        _isActiveDefaultPanel = true;
        foreach (var item in allRectTransformsOnStart)
        {
            if (item.Key == heartsRectTransform)
            {
                Setter.TurnOnCanvasGroup(heartsRectTransform.GetComponent<CanvasGroup>());
                continue;
            }
            item.Key.gameObject.SetActive(true);
        }
        FlyBackButOne(null);
    }
    public void AllDefaultPanelDeactivate(RectTransform forSkip = null)
    {
        foreach (var item in allRectTransformsOnStart)
        {
            if (item.Key == forSkip)
            {
                continue;
            }
            if (item.Key == heartsRectTransform)
            {
                Setter.TurnOffCanvasGroup(heartsRectTransform.GetComponent<CanvasGroup>());
                continue;
            }
            item.Key.gameObject.SetActive(false);
        }
        _isActiveDefaultPanel = false;
    }
    public void MoneyActivate()
    {
        coinsRectTransform.gameObject.SetActive(true);
        DOTween.Kill("flyAway");
        coinsRectTransform.DOAnchorPos(allRectTransformsOnStart[coinsRectTransform], duration).SetId("flyBack");
    }
    public void SmallSettingsActivate()
    {
        MenuActivate(smallSettings);
    }
    public void HeartsActivate()
    {
        heartsRectTransform.gameObject.SetActive(true);
    }
    public void StarsActivate()
    {
        starsRectTransform.gameObject.SetActive(true);
    }
    public void MenuActivate(CanvasGroup SetPanel = null, RectTransform excludeFromFlight = null)
    {
        FlyAwayAllOffButOne(excludeFromFlight);
        Setter.SetCanvasGroup(SetPanel);
    }
    public float MenuDeactivate(CanvasGroup SetPanel = null, RectTransform excludeToFlight = null)
    {
        Setter.SetCanvasGroup(SetPanel);
        FlyBackButOne(excludeToFlight);
        return duration;
    }
    public void MenuDeactivate()
    {
        MenuDeactivate(null);
    }
    public void SettingsMenuActivate()
    {
        MenuActivate(settingsMenu);
    }
    public void VisualNovelActivate(CanvasGroup visualNovel, bool isStarActive = false)
    {
        float startDuration = duration;
        duration = 0f;
        if (isStarActive)
            MenuActivate(visualNovel, starsRectTransform);
        else
            MenuActivate(visualNovel, null);
        duration = startDuration;
    }
    public void HeartsMenuActivate()
    {
        DOTween.Kill("deactivateCoin");
        MenuActivate(heartsMenu, coinsRectTransform);
        coinsRectTransform.DOAnchorPosX(153, duration).SetDelay(duration).SetId("activateCoin");
    }
    public void HeartsMenudDeactivate()
    {
        DOTween.Kill("activateCoin");
        MenuDeactivate(null, coinsRectTransform);
        coinsRectTransform.DOAnchorPosX(_startCoinsValueX, duration / 2).SetId("deactivateCoin");
    }
    public void CoinsMenuActivate()
    {
        DOTween.Kill("deactivateCoin");
        MenuActivate(coinsMenu, coinsRectTransform);
        coinsRectTransform.DOAnchorPosX(153, duration).SetDelay(duration).SetId("activateCoin");
        heartsParticleSystem.gameObject.SetActive(false);
    }
    public void CoinsMenudDeactivate()
    {
        DOTween.Kill("activateCoin");
        MenuDeactivate(null, coinsRectTransform);
        coinsRectTransform.DOAnchorPosX(_startCoinsValueX, duration / 2).SetId("deactivateCoin");
    }
    public void ProfileMenuActivate()
    {
        MenuActivate(profileMenu);
    }
    public void WinMenuActivate(AwardsScriptableObject awardsScriptableObject)
    {
        winMenu.gameObject.SetActive(true);
        _menuWin.Activate(awardsScriptableObject);
        Setter.SetCanvasGroup(winMenu);
        _menuWin.IsActive = true;
    }
    public void WinMenudDeactivate()
    {
        winMenu.gameObject.SetActive(false);
        Setter.SetCanvasGroup(null);
        _menuWin.IsActive = false;
    }
    public void FlyAwayAllOffButOne(RectTransform rectTransform)
    {
        FlyAway(heartsRectTransform, new Vector3(-30, 60));
        if (rectTransform != starsRectTransform)
        {
            FlyAway(starsRectTransform, new Vector2(starsRectTransform.anchoredPosition.x, 60));
        }
        if (rectTransform != coinsRectTransform)
        {
            FlyAway(coinsRectTransform, new Vector2(coinsRectTransform.anchoredPosition.x, 60));

        }
        FlyAway(settingsRectTransform, new Vector3(70, 60));
        FlyAway(taskRectTransform, new Vector3(-100, -100));
        FlyAway(playRectTransform, new Vector3(120, -100));
        FlyAway(boxRectTransform, new Vector3(boxRectTransform.anchoredPosition.x, -100));
    }
    private Sequence FlyAway(RectTransform rectTransform, Vector2 LastPosition)
    {
        DOTween.Kill("flyBack");
        Vector2 moveCenter;
        if (LastPosition.x > 0)
            moveCenter = new Vector2(-10, 0);
        else
            moveCenter = new Vector2(10, 0);
        if (LastPosition.y > 0)
            moveCenter = new Vector2(moveCenter.x, -10);
        else
            moveCenter = new Vector2(moveCenter.x, 10);
        var flyAway = DOTween.Sequence();
        flyAway.Append(rectTransform.DOAnchorPos(rectTransform.anchoredPosition + moveCenter, duration / 3));
        flyAway.Append(rectTransform.DOAnchorPos(LastPosition, duration));
        flyAway.Append(rectTransform.DOAnchorPos(LastPosition, duration));
        flyAway.SetId("flyAway");
        return flyAway;
    }
    private void FlyBackButOne(RectTransform rectTransform)
    {
        DOTween.Kill("flyAway");
        foreach (var item in allRectTransformsOnStart)
        {
            if (rectTransform == item.Key)
            {
                continue;
            }
            item.Key.DOAnchorPos(item.Value, duration).SetId("flyBack");
        }
    }

    private void TurnOffAllButOne(CanvasGroup group)
    {
        foreach (CanvasGroup item in allCanvasGroups)
        {
            if (group == item)
                continue;
            Setter.TurnOffCanvasGroup(item);
        }
    }
    private void TurnOnAll()
    {
        foreach (CanvasGroup item in allCanvasGroups)
        {
            Setter.TurnOnCanvasGroup(item);
        }
    }
    private void InitializeDelegates()
    {
        _initDecreaseComplete = decreaseTask;
        _initDecreaseComplete.Initialize(() => MenuDeactivate(null, starsRectTransform));
        _initDecreaseComplete = decreaseHearts;
        _initDecreaseComplete.Initialize(HeartsMenudDeactivate);
        _initDecreaseComplete = decreaseSettings;
        _initDecreaseComplete.Initialize(() => MenuDeactivate());
        _initDecreaseComplete = decreasePlay;
        _initDecreaseComplete.Initialize(() => MenuDeactivate());
        _initDecreaseComplete = decreaseProfile;
        _initDecreaseComplete.Initialize(() => MenuDeactivate());

        Button taskButton = taskRectTransform.GetComponent<Button>();
        taskButton.onClick.AddListener(() => MenuActivate(taskMenu, starsRectTransform));
        Button settingsButton = settingsRectTransform.GetComponent<Button>();
        settingsButton.onClick.AddListener(() => MenuActivate(settingsMenu));
        Button playButton = playRectTransform.GetComponent<Button>();
        playButton.onClick.AddListener(() => MenuActivate(playMenu));
    }
    private void OnDestroy()
    {
        _initDecreaseComplete = decreaseTask;
        _initDecreaseComplete.Deinitialize(() => MenuDeactivate(null, starsRectTransform));
        _initDecreaseComplete = decreaseHearts;
        _initDecreaseComplete.Deinitialize(HeartsMenudDeactivate);
        _initDecreaseComplete = decreaseSettings;
        _initDecreaseComplete.Deinitialize(() => MenuDeactivate());
        _initDecreaseComplete = decreasePlay;
        _initDecreaseComplete.Deinitialize(() => MenuDeactivate());
    }
}
