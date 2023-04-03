using UnityEngine;

[CreateAssetMenu(fileName = nameof(Levels), menuName = "ScriptableObjects/Level/" + nameof(Levels), order = 1)]
public class Levels : ScriptableObject
{
    [SerializeField] private Level[] _levels;

    public Level this[int index] => _levels[index];
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


