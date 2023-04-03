using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class LevelGoalFilterStorage :IEnumerable<Func<IItem, ItemStateTypes, bool>>
{
    private List<Func<IItem, ItemStateTypes, bool>> _filters = new();
        
    public void FillFilters(IEnumerable<LevelGoal> goals)
    {
        foreach (var goal in goals)
        {
            _filters.Add(goal.TryCount);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Func<IItem, ItemStateTypes, bool>> GetEnumerator()
    {
        return _filters.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}





