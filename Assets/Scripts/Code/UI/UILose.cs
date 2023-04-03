using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILose : MonoBehaviour
{
    public void ReturnInMainMenu()
    {
        LoadLevel.instance.LoudScene(0);
    }
}
