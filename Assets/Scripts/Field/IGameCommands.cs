using UniRx;

namespace Assets.Scripts.Products
{
    public interface IGameCommands
    {
        ReactiveCommand FinishGameCommand { get; }
        ReactiveCommand RestartGameCommand { get; }
    }
}