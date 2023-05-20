using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IItem, IPooleable
{
    private ItemStateMachine _stateMachine;
    private IStateObserver _stateObserver;

    public event UnityAction<IPooleable> Deactivation;

    public int Id { get; private set; } = GameConstatns.NullId;

    public IItemView View { get; private set; }

    public ItemState State => _stateMachine.State;

    public IItem SwapWith { get ; set ; }

    private void Awake()
    {
        View = GetComponentInChildren<IItemView>();
        InitStateMachine();        
    }

    private void OnDestroy()
    {
        _stateMachine.StateChanged -= OnStateChanged;
    }

    public void SetId(int id)
    {
        Id = id;
    }

    public async UniTask MoveNextState(float transitionTime)
    {
        await _stateMachine.MoveOnNextState(transitionTime);
    }

    private void InitStateMachine()
    {
        var states = new States(
            new IceState(View, Id),
            new DeadState(View, this),
            new DefaultState(View),
            new BoostState(View, () => Id),
            new BoxState1(View),
            new BoxState2(View),
            new MovableBox1State(View),
            new MovableBox2State(View));
        _stateMachine = new(states);
        _stateMachine.StateChanged += OnStateChanged;
    }

    public void Deactivate()
    {
        Deactivation?.Invoke(this);
    }

    public void SetState(ItemStateTypes state)
    {
        _stateMachine.InitFirstState(state);
    }

    public void SetStateObserver(IStateObserver itemStateObserver)
    {
        _stateObserver = itemStateObserver;
    }

    protected virtual void OnStateChanged(ItemStateTypes oldState, ItemStateTypes newState)
    {
        _stateObserver?.Observe(this, oldState, newState);        
    }

    public void OnSwap()
    {
        
    }
}
