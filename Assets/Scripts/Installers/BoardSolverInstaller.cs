using System.Collections.Generic;
using Zenject;

public class BoardSolverInstaller : MonoInstaller
{
    private BoardSolver _boardSolver;
    public override void InstallBindings()
    {
        var board = Container.Resolve<Board>();
        var itemSwaper = Container.Resolve<ItemSwaper>();
        var boardFiller = Container.Resolve<IBoardFiller>();
        var sequenceDetectors = Container.Resolve<IEnumerable<ISequenceDetector>>();
        var boostExecuter = Container.Resolve<BoostExicuter>();
        SolveSlotsDetecor solveSlotsDetecor = new(sequenceDetectors, board);


        Workers workers = new(solveSlotsDetecor, board, boardFiller);

        _boardSolver = new(workers, itemSwaper, boostExecuter);

        Container.Bind<BoardSolver>().FromInstance(_boardSolver).AsSingle().NonLazy();
    }

}
