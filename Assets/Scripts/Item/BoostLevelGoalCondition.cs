using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BoostLevelGoalCondition), menuName = "ScriptableObjects/" + nameof(BoostLevelGoalCondition), order = 1)]
public class BoostLevelGoalCondition : LevelGoalConditionBase
{
    [SerializeField] private BoostTypes[] _types = default;    

    public BoostTypes Type { get => _types[0]; }

    public override bool Condition(IItem item, ItemStateTypes _)
    {
        return _types.Any(type => item.Id == (int)type);
    }
}






