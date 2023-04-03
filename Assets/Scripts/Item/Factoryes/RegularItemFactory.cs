using UnityEngine;

[CreateAssetMenu(fileName = nameof(RegularItemFactory), menuName = "ScriptableObjects/" + nameof(RegularItemFactory), order = 1)]
public class RegularItemFactory : TextureMapFactory<int, IItem>
{
    [SerializeField] private Item _itemPrefab;    
    [SerializeField] private IntBinds _idBinds;

    private ObjectPooler<Item> _objectPooler;
    private ConfigPool<int> _itemPool;
    private IStateObserver _itemStateObserver;

    private const int IdForRandom = -1;

    private int[,] _idMap;

    public override void Init(IStateObserver itemStateObserver)
    {
        _objectPooler = new(_itemPrefab);
        _itemStateObserver = itemStateObserver;
    }

    public override void SetMap(Texture2D poolMap)
    {
        _itemPool = new(_idBinds);
        _itemPool.Init(poolMap);
    }

    public override IItem Get(int column)
    {
        var item = _objectPooler.Get();

        InitItem(item, column);

        return item;
    }

    private void InitItem(IItem item, int collumnIndex)
    {
        SetId(item, collumnIndex);

        SetSprite(item);

        item.SetState(ItemStateTypes.Default);

        item.SetStateObserver(_itemStateObserver);

    }

    private void SetId(IItem item, int collumnIndex) => item.SetId(GetItemId(collumnIndex));

    private int GetItemId(int collumnIndex)
    {
        int id = int.MinValue;

        bool isIdExistInPool = _itemPool.Values.Length > collumnIndex && _itemPool.Values[collumnIndex].TryDequeue(out id);

        if (isIdExistInPool && id != _idBinds.Default)
            return id;
        else
            return GetRandomId();

    }

    private int GetRandomId() => Random.Range(0, GameConstatns.ItemColors);

    private void SetSprite(IItem item)
    {
        if (item.Id == IdForRandom)
        {
            item.View.SetSprite(Random.Range(0, GameConstatns.ItemColors));
            return;
        }
        item.View.SetSprite(item.Id);
    }

}


