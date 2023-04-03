using UnityEngine;

[CreateAssetMenu(fileName = nameof(ColorIdLevelGoalCondition), menuName = "ScriptableObjects/" + nameof(ColorIdLevelGoalCondition), order = 1)]
public class ColorIdLevelGoalCondition : LevelGoalConditionBase
{
    [SerializeField] private int _id;

    public int Id { get => _id; }

    public override bool Condition(IItem item, ItemStateTypes _)
    {
        return item.Id == _id;
    }
}






