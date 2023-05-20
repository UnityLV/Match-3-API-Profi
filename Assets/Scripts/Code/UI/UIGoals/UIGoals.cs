///using Match3.App;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIGoals : MonoBehaviour
{
    [SerializeField] private LevelGoalFactory levelGoalFactory;
    [SerializeField] private LevelGoalSpriteBinds _spriteBinds;
    [SerializeField] private AwardsScriptableObject[] awardsScriptableObject;
    [SerializeField] private UIGoal UIGoal;
    [SerializeField] private Matcher matcher;
    private IEnumerable<LevelGoalConditionBase> _levelGoalConditions => levelGoalFactory.LevelGoalConditions;
    private Dictionary<int, UIGoal> uIGoals;
    private int _complitedGoals;
    private CanvasController _canvasController;
    private History _history;
    private bool _isLevelComplete;
    private AwardsScriptableObject _currensAward;

    private void Awake()
    {
        levelGoalFactory.LevelGoalsCreated += OnGoalsCreated;
        matcher.TouchAvailable += OnTouchEnable;
        _canvasController = CanvasController.instance;
        _history = History.instance;
    }

    private void OnGoalsCreated(IEnumerable<LevelGoal> goals)
    {
        uIGoals = new Dictionary<int, UIGoal>();
        foreach (LevelGoal item in goals)
        {
            item.Compleated += OnComplited;
            item.AmountChanged += OnAmountChanged;
            var levelGoalCondition = _levelGoalConditions.ElementAt(item.Index);
            UIGoal uiGoal = CreateGoalImage(levelGoalCondition);
            uIGoals.Add(item.Index, uiGoal);
        }
    }

    private UIGoal CreateGoalImage(LevelGoalConditionBase levelGoalCondition)
    {
        UIGoal uiGoal = Instantiate(UIGoal, this.transform);
        uiGoal.GoalImage.sprite = _spriteBinds.GetSprite(levelGoalCondition);
        uiGoal.CheckMark.sprite = _spriteBinds.DoneSprite;
        uiGoal.AmountText.text = levelGoalCondition.Amount.ToString();
        return uiGoal;
    }

    private void RefreshText(string text, int goalIndex)
    {
        uIGoals[goalIndex].AmountText.text = text;
    }

    private void OnComplited(LevelGoal levelGoal)
    {
        UIGoal goal = uIGoals[levelGoal.Index];
        goal.AmountText.gameObject.SetActive(false);
        goal.CheckMark.gameObject.SetActive(true);
        _complitedGoals++;

        if (uIGoals.Count <= _complitedGoals)
        {
            _currensAward = awardsScriptableObject[levelGoal.Index];
            _canvasController.StarsBank.Add(_currensAward.Stars);

            _canvasController.CoinsBank.Add(_currensAward.Money);
            _history.LevelComplited();
            _isLevelComplete = true;
        }
    }
    private void LevelComplete()
    {
        _canvasController.WinMenuActivate(_currensAward);
        _canvasController.StarsActivate();
        _canvasController.MoneyActivate();
    }
    private void OnTouchEnable()
    {
        if (_isLevelComplete)
        {
            LevelComplete();
        }
    }
    private void OnAmountChanged(LevelGoal levelGoal, int amount)
    {
        RefreshText(amount.ToString(), levelGoal.Index);
    }

}
