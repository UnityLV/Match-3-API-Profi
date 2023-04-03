using Assets.Scripts.Code.Bank;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHeartsPanel : MonoBehaviour
{
    [SerializeField] private Hearts hearts;
    public void Add()
    {
        hearts.Add(1);
    }
    public void Remove()
    {
        hearts.TryRemove(1);
    }
}
