using TMPro;
using UnityEngine;

public class UIMoves : MonoBehaviour
{
    [SerializeField] private MoveCounter moveCounter;
    [SerializeField] private TMP_Text movesText;
    [SerializeField] private CanvasGroup menuLose;
    [SerializeField] private CanvasGroup gamePanel;
    [SerializeField] private int movesToAdd = 5;
    [SerializeField] private Matcher matcher;

    private CanvasController _canvasController;
    private bool _isLevelComplete;

    private void Awake()
    {
        moveCounter.MovesCountUpdated += OnMovesUpdated;
        moveCounter.AllMovesEnded += OnMovesEnded;
        _canvasController = CanvasController.instance;
        matcher.TouchAvailable += OnTouchEnable;
    }
    private void OnMovesUpdated(int count)
    {
        movesText.text = count.ToString();
    }

    private void OnMovesEnded()
    {
        _isLevelComplete = true;
    }
    private void OnDestroy()
    {
        moveCounter.MovesCountUpdated -= OnMovesUpdated;
        moveCounter.AllMovesEnded -= OnMovesEnded;
    }
    public void BuyMoves(int cost)
    {
        if (_canvasController.CoinsBank.TryRemove(cost))
        {
            moveCounter.AddMoves(movesToAdd);
            _canvasController.MenuDeactivate(menuLose);
            _canvasController.MenuActivate(gamePanel);
            _canvasController.AllDefaultPanelDeactivate();
        }
    }
    private void OnTouchEnable()
    {
        if (_isLevelComplete)
        {
            MovesEnded();
            _canvasController.HeartsBank.TryRemove(1);
        }
    }
    private void MovesEnded()
    {
        _canvasController.HeartsActivate();
        _canvasController.MenuDeactivate(gamePanel);
        _canvasController.MenuActivate(menuLose);
        _canvasController.MoneyActivate();
    }
}
