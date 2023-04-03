using UnityEngine;
using UnityEngine.Events;

public class ItemStateObserver : MonoBehaviour, IStateObserver
{
    public event UnityAction<IItem, ItemStateTypes, ItemStateTypes> ItemChangedState;

    public void Observe(IItem item, ItemStateTypes oldItemState, ItemStateTypes newItemState)
    {
        if (GameConstatns.IsNeedToDebugObservableStates)
        {
            Debug.Log("Id " +  item.Id + " Old: " + oldItemState + " New: " + newItemState);
        }
        ItemChangedState?.Invoke(item, oldItemState, newItemState);
    }
}






