using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public class ItemNextStateMover
{
    public async UniTask SetNextStateSequence(MatchSequence sequence)
    {
        switch (sequence.Type)
        {
            case SequenceTypes.Three:
                await SetNextStateRegularSequence(sequence);
                return;
            case SequenceTypes.FourHorisontal:
            case SequenceTypes.FourVertical:
            case SequenceTypes.Square:
            case SequenceTypes.FiveLine:
            case SequenceTypes.LShape:
            case SequenceTypes.TShape:
                await SetNextStateBoostSequence(sequence);
                break;
        }

    }

    private async UniTask SetNextStateRegularSequence(MatchSequence sequence)
    {
        float removeTIme = 0.2f;

        await UniTask.WhenAll(sequence.Get().Select(item => MoveNextStateItemIn(item, removeTIme)).ToArray());
    }

    private async UniTask SetNextStateBoostSequence(MatchSequence sequence)
    {       
        float removeTime = 0.14f;

        await UniTask.WhenAll(sequence.Get().Select(item => MoveNextStateItemIn(item, removeTime, sequence.Origin.GridPosition)).ToArray());
    }

    public async UniTask MoveNextStateItemIn(ICell cell, float transitionTime = 0, params Vector3[] movePath)
    {
        if (cell.HasItem)
        {
            await UniTask.WhenAll(cell.Item.MoveNextState(transitionTime),
                TryMoveOnPath(cell.Item, transitionTime, movePath));   
        }
    }

    private async UniTask TryMoveOnPath(IItem item, float time, Vector3[] removePath)
    {
        if (removePath.Length > 0 && item.State.IsAlive == false)
        {
            await item.View.MoveOn(time,removePath);
        }
    }
}
