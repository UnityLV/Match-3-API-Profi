using DG.Tweening;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private CanvasGroup startGame;
    [SerializeField] CanvasController _canvasController;
    [SerializeField] History _history;
    [SerializeField] private float durationFade = 0.05f;
    [SerializeField] private RectTransform settingsButton;


    private static bool _isStart = true;

    private void Start()
    {
        if (_isStart)
        {
            _isStart = false;
            _canvasController.AllDefaultPanelDeactivate(settingsButton);
        }
        else
        {
            _canvasController.Setter.TurnOffCanvasGroup(startGame);
        }
    }
    public void OnPlayClick()
    {
        startGame.DOFade(0, durationFade).OnKill(() =>
        {
            _canvasController.AllDefaultPanelActivate();
            _canvasController.Setter.TurnOffCanvasGroup(startGame);
            _history.OnStartGame();
        });
    }
}
