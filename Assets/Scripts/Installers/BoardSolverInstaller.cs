using System.Collections.Generic;
using Zenject;

public class BoardSolverInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var board = Container.Resolve<Board>();
        var itemSwaper = Container.Resolve<ItemSwaper>();
        var boardFiller = Container.Resolve<IBoardFiller>();
        var sequenceDetectors = Container.Resolve<IEnumerable<ISequenceDetector>>();
        var boostExecuter = Container.Resolve<IBoostExicuter>();
        SolveSlotsDetecor solveSlotsDetecor = new(sequenceDetectors, board);


        Workers workers = new(solveSlotsDetecor, board, boardFiller);

        BoardSolver boardSolver = new(workers, itemSwaper, boostExecuter);

        Container.Bind<BoardSolver>().FromInstance(boardSolver).AsSingle().NonLazy();
    }

}
