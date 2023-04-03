using UnityEngine;

public abstract class LevelGoalConditionBase : ScriptableObject
{
    [SerializeField] private int _goalAmount = GameConstatns.DefaultitemGoalAmount;    

    public int Amount { get => _goalAmount; }    

    public abstract bool Condition(IItem item, ItemStateTypes type);
}






