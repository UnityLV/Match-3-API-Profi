using Zenject;

public class BoardInstaller : MonoInstaller
{
    private Board _board;

    public override void InstallBindings()
    {
        _board = new(Container.Resolve<CellFactory>());
        _board.CreateCells();

        Container.Bind<Board>().FromInstance(_board).AsSingle();
    }
}
