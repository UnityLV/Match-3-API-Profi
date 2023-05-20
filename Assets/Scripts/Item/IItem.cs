using Cysharp.Threading.Tasks;

public interface IItem
{
    ItemState State { get; }
    int Id { get; }
    IItemView View { get; }
    IItem SwapWith { get; set; }
    void SetId(int id);
    UniTask MoveNextState(float transitionTime = 0);
    void SetState(ItemStateTypes state);
    void OnSwap();
    void SetStateObserver(IStateObserver itemStateObserver);
}
