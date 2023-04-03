using System.Collections.Generic;
using Zenject;

public class SequenceDetectorsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IEnumerable<ISequenceDetector>>().FromMethod(GetSequences).AsSingle().NonLazy();
    }

    private IEnumerable<ISequenceDetector> GetSequences()
    {
        var board = Container.Resolve<Board>();

        return new List<ISequenceDetector>
        {
            new SequenceFiveLineDetector(board),
            new LShapeDetector(board),
            new TShapeDetector(board),
            new SquareDetector(board),
            new SequenceFourHorisontalDetector(board),
            new SequenceFourVerticalDetector(board),
            new SequenceThreeDetector (board),
        };
    }
}
