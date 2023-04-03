using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GoalCellOnBoardFinder : MonoBehaviour
{

    [SerializeField] private LevelGoalFactory _levelGoalFactory;

    [Inject]
    private Board _board;
    private IEnumerable<LevelGoal> _goals;

    private void OnEnable()
    {
        _levelGoalFactory.LevelGoalsCreated += OnLevelGoalsCreated;
    }

    private void OnDisable()
    {
        _levelGoalFactory.LevelGoalsCreated -= OnLevelGoalsCreated;
    }

    private void OnLevelGoalsCreated(IEnumerable<LevelGoal> goals)
    {
        _goals = goals;
    }

    public bool TryFind(out ICell item)
    {
        return (item = Find()) != default;
    }

    private ICell Find()
    {
        foreach (ICell cell in _board.Randomize())
        {
            if (cell.HasItem)
            {
                foreach (var goal in _goals)
                {
                    if (goal.IsComplete == false &&
                        goal.IsPassFilter(cell.Item, cell.Item.State.Type))
                    {
                        return cell;
                    }
                }
            }            
        }
        return default;
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


