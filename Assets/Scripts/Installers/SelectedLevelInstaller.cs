using UnityEngine;
using Zenject;

public class SelectedLevelInstaller : MonoInstaller
{
    [SerializeField] private Levels _levels;
    public override void InstallBindings()
    {
        Level level = _levels[LevelSelector.SelectedLevel];
        Container.Bind<Level>().FromInstance(level).AsSingle().NonLazy();
    }
}
