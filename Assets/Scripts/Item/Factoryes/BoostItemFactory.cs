using UnityEngine;


[CreateAssetMenu(fileName = nameof(BoostItemFactory), menuName = "ScriptableObjects/" + nameof(BoostItemFactory), order = 1)]
public class BoostItemFactory : ConfigFactory<BoostTypes, IBoostItem>
{
    [SerializeField] private BoostItem _boostPrefab;

    private ObjectPooler<BoostItem> _objectPooler;
    private IStateObserver _itemStateObserver;

    public override void Init(IStateObserver itemStateObserver)
    {
        _objectPooler = new(_boostPrefab);
        _itemStateObserver = itemStateObserver;
    }

    public override IBoostItem Get(BoostTypes type)
    {
        IBoostItem item = _objectPooler.Get();
        InitBoost(item, type);
        return item;
    }

    private void InitBoost(IBoostItem boostItem, BoostTypes type)
    {
        boostItem.SetId((int)type);      
        boostItem.SetBoostType(type);        
        boostItem.SetState(ItemStateTypes.Boost);
        boostItem.SetStateObserver(_itemStateObserver);
    }



    
}



