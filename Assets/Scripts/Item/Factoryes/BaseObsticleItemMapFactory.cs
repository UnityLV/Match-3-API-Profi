using UnityEngine;

public abstract class BaseObsticleItemMapFactory : TextureMapFactory<int, IItem>
{
    [SerializeField] private Item _itemPrefab;
    [SerializeField] private IntBinds _binds;

    private ConfigPool<int> _configPool;
    private ObjectPooler<Item> _objectPooler;

    protected IStateObserver ItemStateObserver;
    protected ItemStateTypes FactoryType;
    protected int Default => _binds.Default;

    public override void Init(IStateObserver itemStateObserver)
    {
        _objectPooler = new(_itemPrefab);
        ItemStateObserver = itemStateObserver;
    }

    public override void SetMap(Texture2D poolMap)
    {
        _configPool = new(_binds);
        _configPool.Init(poolMap);
    }

    protected bool TryGetFromConfigPool(int column, out int id)
    {
        return _configPool.Values[column].TryDequeue(out id);
    }

    protected Item GetItemFromPool()
    {
        return _objectPooler.Get();
    }



}
