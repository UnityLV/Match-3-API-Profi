using Cysharp.Threading.Tasks;
using UnityEngine.Events;

public interface ISwapSolwer
{
    UniTask SolveSwap(ICell selectedCell, ICell cell);    
}




