using System.Collections.Generic;
using System.Linq;


public class MatchSequence
{
    private HashSet<ICell> _cells;

    public MatchSequence() { _cells = new(); }

    public MatchSequence(IEnumerable<ICell> value) => _cells = value.ToHashSet();

    public ICell Origin { get; set; }

    public SequenceTypes Type { get; set; }

    public int Id => _cells.ElementAt(0).Item.Id;

    public IEnumerable<ICell> Get() => _cells;

    public void Add(IEnumerable<ICell> sequence) => _cells = new HashSet<ICell>(_cells.Concat(sequence));

    public void Add(MatchSequence sequence) => _cells = new HashSet<ICell>(_cells.Concat(sequence.Get()));

    public IEnumerator<ICell> GetEnumerator() => _cells.GetEnumerator();
}

