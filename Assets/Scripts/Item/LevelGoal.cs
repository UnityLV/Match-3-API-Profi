using System;
using UnityEngine.Events;

public class LevelGoal
{
    private Func<IItem, ItemStateTypes, bool> _filter;
    private int _counter = GameConstatns.DefaultitemGoalAmount;

    public event UnityAction<LevelGoal> Compleated;
    public event UnityAction<LevelGoal, int> AmountChanged;
    public int Index { get; }
    public bool IsComplete { get; private set; }

    public LevelGoal(Func<IItem, ItemStateTypes, bool> filer, int amount, int index)
    {
        _filter = filer;
        _counter = amount;
        Index = index;
    }

    public bool TryCount(IItem item, ItemStateTypes oldItemState)
    {
        if (_filter(item, oldItemState))
        {
            Count();
            return true;
        }

        return false;
    }

    public bool IsPassFilter(IItem item, ItemStateTypes oldItemState)
    {
        return _filter(item, oldItemState);
    }

    private void Count()
    {
        _counter--;
        AmountChanged?.Invoke(this, _counter);
        if (_counter <= 0 && IsComplete == false)
        {
            Compleated?.Invoke(this);
            Compleated = null;
            IsComplete = true;
        }
    }
}






