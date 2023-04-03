using System.Collections.Generic;
using Zenject;

public class BoardFillerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var itemFactory = Container.Resolve<TextureMapFactory<int, IItem>>();
        var boostFactory = Container.Resolve<ConfigFactory<BoostTypes, IBoostItem>>();
        var obstacleFactories = Container.Resolve<IEnumerable<TextureMapFactory<int, IItem>>>();
        var board = Container.Resolve<Board>();
        var boardFiller = new BoardFiller(itemFactory, boostFactory, obstacleFactories, board);

        Container.Bind<IBoardFiller>().FromInstance(boardFiller).AsSingle().NonLazy();
    }
}
