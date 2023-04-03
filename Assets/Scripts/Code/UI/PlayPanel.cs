using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayPanel : MonoBehaviour
{
    [SerializeField] private Levels levels;
    [SerializeField] private PlayGoal playGoal;
    [SerializeField] private LevelGoalSpriteBinds spriteBinds;
    private History _history;
    private List<PlayGoal> goals;
    private LoadLevel _loadLevel;
    private bool _isActivate;
    private void Start()
    {
        goals = new();
        _history = History.instance;
        _loadLevel = LoadLevel.instance;
        _loadLevel.Initialize(OnDeactivate);
    }
    public void OnActivate()
    {
        if (_isActivate)
        {
            return;
        }
        _isActivate = true;
       IEnumerable<LevelGoalConditionBase> levelGoal = levels[_history.CurrentLevelID].LevelGoalConditions;
        foreach (var item in levelGoal)
        {
            PlayGoal goal = Instantiate(playGoal,this.transform);
            goal.GoalImage.sprite = spriteBinds.GetSprite(item);
            goal.AmountText.text = item.Amount.ToString();
            goals.Add(goal);
        }
    }
    public void OnDeactivate(int level)
    {
        _isActivate = false;
        if (level == 0)
        {
            foreach (var item in goals)
            {
                if (item == null)
                {
                    continue;
                }
                Destroy(item.gameObject);
            }
        }
    }
    private void OnDestroy()
    {
        _loadLevel.Deinitialize(OnDeactivate);
    }
}
