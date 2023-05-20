using Zenject;
using UnityEngine;

public class BoostExicuterInstaller : MonoInstaller
{
    [SerializeField] private GoalCellOnBoardFinder _cellOnBoardFinder;

    public override void InstallBindings()
    {
        var board = Container.Resolve<Board>();

        BoostExicuter boostExicuter = new(board, _cellOnBoardFinder);

        Container.Bind<IBoostExicuter>().FromInstance(boostExicuter).AsSingle().NonLazy();
    }
}
