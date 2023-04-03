using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Level), menuName = "ScriptableObjects/Level/" + nameof(Level), order = 1)]
public class Level : ScriptableObject
{
    [SerializeField] private int _moves;
    [SerializeField] private LevelGoalConditionBase[] _levelGoalConditions;
    [SerializeField] private Texture2D _poolMap;
    [SerializeField] private Texture2D _cellMap;

    public IEnumerable<LevelGoalConditionBase> LevelGoalConditions { get => _levelGoalConditions; }
    public Texture2D PoolMap { get => _poolMap; }
    public Texture2D CellMap { get => _cellMap; }
    public int Moves { get => _moves; }
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


