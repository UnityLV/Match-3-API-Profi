using Zenject;
using UnityEngine;

public class BoostExicuterInstaller : MonoInstaller
{
    [SerializeField] private GoalCellOnBoardFinder _cellOnBoardFinder;
    private BoostExicuter _boostExicuter;

    public override void InstallBindings()
    {
        var board = Container.Resolve<Board>();
        var _itemStateMover = new ItemNextStateMover();

        _boostExicuter = new(_itemStateMover, board, _cellOnBoardFinder);

        Container.Bind<BoostExicuter>().FromInstance(_boostExicuter).AsSingle().NonLazy();
    }
}
