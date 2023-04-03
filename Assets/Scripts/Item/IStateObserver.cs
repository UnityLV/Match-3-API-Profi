public interface IStateObserver
{
    void Observe(IItem item, ItemStateTypes oldItemState, ItemStateTypes newItemState);
}
