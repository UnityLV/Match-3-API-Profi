using System.Collections.Generic;
using UnityEngine.Events;
using System;
using UnityEngine;
using System.Linq;

public class LevelGoalsReachCounter 
{
    private int _goalCounter;

    public event UnityAction AllGoalsReached;

    public LevelGoalsReachCounter(IEnumerable<LevelGoal> levelGoals)
    {
        _goalCounter = levelGoals.Count();

        foreach (var levelGoal in levelGoals)
        {
            levelGoal.Compleated += OnCompleated;
        }
    }   

    private void OnCompleated(LevelGoal levelGoal)
    {
        levelGoal.Compleated -= OnCompleated;
        HandleCompleatedGoal(levelGoal);
    }

    private void HandleCompleatedGoal(LevelGoal levelGoal)
    {
        Debug.Log(levelGoal.Index + " Цель достигнута");

        _goalCounter--;

        if (_goalCounter <= 0)
        {
            AllGoalsReached?.Invoke();
        }
    }
}






