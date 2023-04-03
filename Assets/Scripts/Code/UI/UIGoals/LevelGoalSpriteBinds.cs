using UnityEngine;

[CreateAssetMenu(fileName = nameof(LevelGoalSpriteBinds), menuName = "ScriptableObjects/" + nameof(LevelGoalSpriteBinds), order = 1)]
public class LevelGoalSpriteBinds : ScriptableObject
{
    [field: SerializeField] public Sprite DoneSprite { get; private set; }
    [SerializeField] private Sprite _defaultSprite;

    [SerializeField] private IdSpriteBind[] _itemIdSpriteBind;
    [SerializeField] private ItemStateTypesSpriteBind[] _itemStateTypesSpriteBind;
    [SerializeField] private BoostSpriteBind[] _boostSpriteBinds;

    public Sprite GetSprite(LevelGoalConditionBase levelGoalCondition)
    {
        if (levelGoalCondition is ColorIdLevelGoalCondition colorCondition)
        {
            return GetSprite(colorCondition.Id);
        }
        if (levelGoalCondition is BoostLevelGoalCondition boostCondition)
        {
            return GetSprite(boostCondition.Type);
        }
        if (levelGoalCondition is StateLevelGoalCondition stateCondition)
        {
            return GetSprite(stateCondition.State);
        }
        return _defaultSprite;
    }

    private Sprite GetSprite(int id)
    {
        foreach (var bind in _itemIdSpriteBind)
        {
            if (bind.Id == id)
            {
                return bind.Sprite;
            }
        }

        return _defaultSprite;
    }

    private Sprite GetSprite(ItemStateTypes state)
    {
        foreach (var bind in _itemStateTypesSpriteBind)
        {
            if (bind.State == state)
            {
                return bind.Sprite;
            }
        }

        return _defaultSprite;
    }

    private Sprite GetSprite(BoostTypes type)
    {
        foreach (var bind in _boostSpriteBinds)
        {
            if (bind.Type == type)
            {
                return bind.Sprite;
            }
        }

        return _defaultSprite;
    }
     

    
}
