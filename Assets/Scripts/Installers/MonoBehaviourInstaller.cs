using UnityEngine;
using Zenject;

public class MonoBehaviourInstaller : MonoInstaller
{
    [SerializeField] private Matcher _matcher;
    [SerializeField] private ItemSwaper _itemSwaper;
    
    public override void InstallBindings()
    {       
        Container.Bind<Matcher>().FromInstance(_matcher).AsSingle().NonLazy();

        Container.Bind<ItemSwaper>().FromInstance(_itemSwaper).AsSingle().NonLazy();      
    }
        
}
