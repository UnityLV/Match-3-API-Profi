using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TaskScriptableObject", menuName = "ScriptableObjects/TaskScriptableObject")]
public class TaskScriptableObject : ScriptableObject
{
    [SerializeField] private Sprite taskImage;
    [SerializeField] private string task;
    [SerializeField] private int cost = 1;
    [SerializeField] private int dialogNumber;
    public Sprite TaskImage => taskImage;
    public string Task => task;
    public int Cost => cost;
    public int DialogNumber => dialogNumber;
}
