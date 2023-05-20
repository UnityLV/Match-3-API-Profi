using UnityEngine;

public class UISettings : MonoBehaviour
{
    private CanvasController _canvasController;
    private void Start()
    {
        _canvasController = CanvasController.instance;
    }
    public void SettingsActive()
    {
        _canvasController.SmallSettingsActivate();
    }
}
