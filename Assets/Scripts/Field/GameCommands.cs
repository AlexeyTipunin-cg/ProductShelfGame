using UniRx;


namespace Assets.Scripts.Products
{
    public class GameCommands : IGameCommands
    {
        public ReactiveCommand FinishGameCommand { get; }
        public ReactiveCommand RestartGameCommand { get; }

        public ReactiveCommand OnSuccsesfullSwap { get; }

        public ReactiveCommand OnWrongSwap { get; }

        public ReactiveCommand OnSwapEnd { get; }

        public GameCommands()
        {
            RestartGameCommand = new ReactiveCommand();
            FinishGameCommand = new ReactiveCommand();
            OnSuccsesfullSwap = new ReactiveCommand();
            OnWrongSwap = new ReactiveCommand();
            OnSwapEnd = new ReactiveCommand();
        }
    }
}
