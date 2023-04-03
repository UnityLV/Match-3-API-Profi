using System.Collections.Generic;
using UnityEngine;

public class GameStoper : MonoBehaviour
{
    [SerializeField] private LevelGoalFactory _goalFactory;
    [SerializeField] private MoveCounter _moveCounter;

    private LevelGoalsReachCounter _allGoalsDetector;

    private void OnEnable()
    {
        _goalFactory.LevelGoalsCreated += OnLevelGoalsCreated;
        _moveCounter.AllMovesEnded += OnMovesEnded;
    }


    private void OnDisable()
    {
        _goalFactory.LevelGoalsCreated -= OnLevelGoalsCreated;
        _moveCounter.AllMovesEnded -= OnMovesEnded;
    }

    private void OnDestroy()
    {
        _allGoalsDetector.AllGoalsReached -= OnAllGoalsReached;
    }

    private void OnLevelGoalsCreated(IEnumerable<LevelGoal> goals)
    {
        _allGoalsDetector = new(goals);
        _allGoalsDetector.AllGoalsReached += OnAllGoalsReached;
    }

    private void OnAllGoalsReached()
    {
        Debug.Log("Все цели достигнуты");
    }   

    private void OnMovesEnded()
    {
        Debug.Log("Ходы кончились");
    }

}






