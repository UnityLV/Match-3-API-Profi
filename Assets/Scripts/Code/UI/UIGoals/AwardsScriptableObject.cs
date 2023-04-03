using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AwardsScriptableObject", menuName = "ScriptableObjects/AwardsScriptableObject")]
public class AwardsScriptableObject : ScriptableObject
{
    [SerializeField] private int stars = 1;
    [SerializeField] private int money = 50;
    public int Stars => stars;  
    public int Money => money;  
}
