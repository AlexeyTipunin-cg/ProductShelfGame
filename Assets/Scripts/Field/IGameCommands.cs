using UniRx;

namespace Assets.Scripts.Products
{
    public interface IGameCommands
    {
        ReactiveCommand FinishGameCommand { get; }
        ReactiveCommand RestartGameCommand { get; }

        ReactiveCommand OnSuccsesfullSwap { get; }

        ReactiveCommand OnWrongSwap { get; }

        ReactiveCommand OnSwapEnd { get; }

    }
}