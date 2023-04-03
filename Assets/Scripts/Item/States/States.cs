public class States
{
    public States(ItemState iceState, ItemState deadState, ItemState defaultState, ItemState boostState, ItemState boxState1, ItemState boxState2, ItemState movableBox1State, ItemState movableBox2State)
    {
        IceState = iceState;
        DeadState = deadState;
        DefaultState = defaultState;
        BoostState = boostState;
        BoxState1 = boxState1;
        BoxState2 = boxState2;
        MovableBox1State = movableBox1State;
        MovableBox2State = movableBox2State;
    }

    public ItemState IceState { get; }
    public ItemState DeadState { get; }
    public ItemState DefaultState { get; }
    public ItemState BoostState { get; }
    public ItemState BoxState1 { get; }
    public ItemState BoxState2 { get; }
    public ItemState MovableBox1State { get; }
    public ItemState MovableBox2State { get; }

    
}


