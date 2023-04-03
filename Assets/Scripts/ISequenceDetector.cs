using System.Collections.Generic;
using System.Runtime.CompilerServices;

public interface ISequenceDetector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool TryGetSequence(GridPosition position, out MatchSequence sequence);
    
}
