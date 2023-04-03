using System.Collections.Generic;
using Zenject;

public class BoardSolverInstaller : MonoInstaller
{
    private SolveSlotsDetecor _solveSlotsDetecor;
    private BoardSolver _boardSolver;
    public override void InstallBindings()
    {
        var board = Container.Resolve<Board>();
        var itemSwaper = Container.Resolve<ItemSwaper>();
        var boardFiller = Container.Resolve<IBoardFiller>();
        var sequenceDetectors = Container.Resolve<IEnumerable<ISequenceDetector>>();
        var boostExecuter = Container.Resolve<BoostExicuter>();

        _solveSlotsDetecor = new(sequenceDetectors, board);
        _boardSolver = new(_solveSlotsDetecor, itemSwaper, boardFiller, board, boostExecuter);

        Container.Bind<BoardSolver>().FromInstance(_boardSolver).AsSingle().NonLazy();
    }

}
