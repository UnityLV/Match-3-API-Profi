using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class MoveCounter : MonoBehaviour
{
    [Inject] private Level _level;
    [Inject] private BoardSolver _boardSolver;

    private int _moves;

    public event UnityAction AllMovesEnded;
    public event UnityAction<int> MovesCountUpdated;

    private void Awake()
    {
        _moves = _level.Moves;
    }
    private void Start()
    {
        MovesCountUpdated?.Invoke(_moves);
    }

    private void OnEnable()
    {
        _boardSolver.CountedSwapMaked += OnCountedSwapMaked;
    }

    private void OnDisable()
    {
        _boardSolver.CountedSwapMaked -= OnCountedSwapMaked;
    }

    public void AddMoves(int movesToAdd)
    {
        _moves += movesToAdd;
        MovesCountUpdated?.Invoke(_moves);
    }
    private void OnCountedSwapMaked()
    {
        _moves--;

        MovesCountUpdated?.Invoke(_moves);

        CheckEndMoves();
    }

    private void CheckEndMoves()
    {
        if (_moves <= 0)
        {
            AllMovesEnded?.Invoke();
            AllMovesEnded = null;
        }
    }
}