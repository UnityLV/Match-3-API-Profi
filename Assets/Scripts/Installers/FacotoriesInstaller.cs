using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FacotoriesInstaller : MonoInstaller
{
    [SerializeField] private ItemStateObserver _itemStateObserver;
    [SerializeField] private CellFactory _cellFactory;
    [SerializeField] private TextureMapFactory<int, IItem> _itemFactory;
    [SerializeField] private TextureMapFactory<int, IItem>[] _obsticleFactories;
    [SerializeField] private ConfigFactory<BoostTypes, IBoostItem> _boostFactory;

    public override void InstallBindings()
    {
        InitFactories();

        BindFactories();
    }

    private void BindFactories()
    {
        Container.Bind<CellFactory>().FromInstance(_cellFactory).AsSingle().NonLazy();

        Container.Bind<TextureMapFactory<int, IItem>>().FromInstance(_itemFactory).AsSingle().NonLazy();

        Container.Bind<IEnumerable<TextureMapFactory<int, IItem>>>().FromInstance(_obsticleFactories).AsSingle().NonLazy();

        Container.Bind<ConfigFactory<BoostTypes, IBoostItem>>().FromInstance(_boostFactory).AsSingle().NonLazy();
    }

    private void InitFactories()
    {
        Texture2D poolMap, celllMap;
        GetMaps(out poolMap, out celllMap);

        _itemFactory.Init(_itemStateObserver);
        _cellFactory.Init(_itemStateObserver);
        _boostFactory.Init(_itemStateObserver);

        foreach (var obstileFactory in _obsticleFactories)
        {
            obstileFactory.Init(_itemStateObserver);
            obstileFactory.SetMap(celllMap);
        }

        _itemFactory.SetMap(poolMap);
        _cellFactory.SetMap(celllMap);
    }

    private void GetMaps(out Texture2D poolMap, out Texture2D celllMap)
    {
        Level selectedLevel = Container.Resolve<Level>();
        poolMap = selectedLevel.PoolMap;
        celllMap = selectedLevel.CellMap;
    }
}
