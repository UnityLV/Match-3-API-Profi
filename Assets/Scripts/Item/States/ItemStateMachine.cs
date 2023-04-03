using Cysharp.Threading.Tasks;
using UnityEngine.Events;

public class ItemStateMachine
{
    public ItemState State { get; private set; }

    private States _states;

    public event UnityAction<ItemStateTypes, ItemStateTypes> StateChanged;

    public ItemStateMachine(States states)
    {
        _states = states;
    }

    public async UniTask MoveOnNextState(float transitionTime = 0.1f)
    {
        ItemStateTypes old = State.Type;
        await State.Exit(transitionTime);

        State = GetNextState(State.Type);

        await State.Entrance(transitionTime);
        ItemStateTypes @new = State.Type;
        StateChanged?.Invoke(old, @new);

    }

    public void InitFirstState(ItemStateTypes state)
    {
        switch (state)
        {
            case ItemStateTypes.Default:
                State = _states.DefaultState;
                break;
            case ItemStateTypes.Dead:
                State = _states.DeadState;
                break;
            case ItemStateTypes.Ice:
                State = _states.IceState;
                break;
            case ItemStateTypes.Boost:
                State = _states.BoostState;
                break;
            case ItemStateTypes.Box1:
                State = _states.BoxState1;
                break;
            case ItemStateTypes.Box2:
                State = _states.BoxState2;
                break;
            case ItemStateTypes.MovableBox1:
                State = _states.MovableBox1State;
                break;
            case ItemStateTypes.MovableBox2:
                State = _states.MovableBox2State;
                break;
        }

        State.Entrance();
    }

    private ItemState GetNextState(ItemStateTypes type)
    {
        switch (type)
        {
            case ItemStateTypes.Ice:
                return _states.DefaultState;
            case ItemStateTypes.Box2:
                return _states.BoxState1;            
            case ItemStateTypes.MovableBox2:
                return _states.MovableBox1State;
            default:
                return _states.DeadState;
        }
    }
}


