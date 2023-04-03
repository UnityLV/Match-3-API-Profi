using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class LevelGoalFactory : MonoBehaviour
{
    [SerializeField] private ItemStateObserver _stateObserver;
    [Inject] private Level _level;

    private List<LevelGoal> _levelGoals;
    private LevelGoalConditionBase[] _levelGoalConditions;

    public event UnityAction<IEnumerable<LevelGoal>> LevelGoalsCreated;
    public IEnumerable<LevelGoalConditionBase> LevelGoalConditions => _levelGoalConditions;

    private void Awake()
    {
        _levelGoalConditions = _level.LevelGoalConditions.ToArray();
    }

    private void Start()
    {
        ConstructLevelGoals();
    }

    private void OnEnable()
    {
        _stateObserver.ItemChangedState += OnItemChangedState;
    }

    private void OnDisable()
    {
        _stateObserver.ItemChangedState -= OnItemChangedState;
    }

    private void OnItemChangedState(IItem item, ItemStateTypes old, ItemStateTypes @new)
    {
        foreach (var levelGoal in _levelGoals)
        {
            levelGoal.TryCount(item, old);
        }
    }

    private void ConstructLevelGoals()
    {
        _levelGoals = new();

        for (int goalIndex = 0; goalIndex < _levelGoalConditions.Length; goalIndex++)
        {
            LevelGoal levelGoal = new(_levelGoalConditions[goalIndex].Condition, _levelGoalConditions[goalIndex].Amount, goalIndex);
            _levelGoals.Add(levelGoal);
        }

        LevelGoalsCreated?.Invoke(_levelGoals);
    }
}


//    color 0
//    color 1
//    color 2
//    color 3

//public enum SequenceTypes
//{
//   
//
//    Three = 10,
//    FourHorisontal = 11,
//    FourVertical = 12,
//    Square = 13,
//    FiveLine = 14,
//    TShape = 15,
//    LShape = 16
//}
//public enum ItemStateTypes
//{
//    Default = 100,
//    Dead = 101,
//    Ice = 102,
//    Boost = 103,
//    Box1 = 104,
//    Box2 = 105,
//    MovableBox1 = 106,
//    MovableBox2 = 107,
//}


