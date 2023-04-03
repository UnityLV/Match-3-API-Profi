using UnityEngine;


[CreateAssetMenu(fileName = nameof(StateLevelGoalCondition), menuName = "ScriptableObjects/" + nameof(StateLevelGoalCondition), order = 1)]

public class StateLevelGoalCondition : LevelGoalConditionBase
{
    [SerializeField] private ItemStateTypes _state;

    public ItemStateTypes State { get => _state; }

    public override bool Condition(IItem _, ItemStateTypes oldItemState)
    {
        return oldItemState == _state;
    }
}






