using Cysharp.Threading.Tasks;
using System.Linq;

public class ObstilcleSolver
{
    private IObsticleExecuter[] _executers;
    private Board _board;  

    public ObstilcleSolver(Board board)
    {
        _board = board;

        _executers = new IObsticleExecuter[]
        {
            new Box1ObsticleSolver(_board),
            new Box2ObsticleSolver(_board)
        };
    }

    public async UniTask SolveAll(MatchSequence cequence)
    {
        await UniTask.WhenAll(_executers.Select(ex => ex.Execute(cequence)).ToArray());
    }


}
