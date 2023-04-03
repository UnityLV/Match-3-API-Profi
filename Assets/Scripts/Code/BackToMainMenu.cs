using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMainMenu : MonoBehaviour
{
    private LoadLevel _loudLevel;
    private CanvasController _canvasControl;

    void Start()
    {
        _loudLevel = LoadLevel.instance;
        _canvasControl = CanvasController.instance;
    }

    public void Load()
    {
        _loudLevel.LoudScene(0);
        _canvasControl.MenuDeactivate();

    }
}
