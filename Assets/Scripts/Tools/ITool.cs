using Cysharp.Threading.Tasks;
using UnityEngine.Events;

public interface ITool
{
    UniTask Execute();
    void Forget();

    event UnityAction ExecuteStarted;
}
