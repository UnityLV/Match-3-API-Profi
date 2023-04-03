using Cysharp.Threading.Tasks;
using UnityEngine.Events;

public class BoostView : ItemView
{
    public event UnityAction Hided;
    public event UnityAction Showed;

    public override async UniTask Hide(float hideTime = 0.1F)
    {
        Hided?.Invoke();
    }

    public override void Show()
    {
        Showed?.Invoke();
    }
}
