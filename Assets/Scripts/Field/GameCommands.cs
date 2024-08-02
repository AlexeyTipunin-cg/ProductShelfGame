using System;
using UniRx;


namespace Assets.Scripts.Products
{
    public class GameCommands
    {
        public ReactiveCommand FinishGameCommand { get; }
        public ReactiveCommand RestartGameCommand { get;}
        public GameCommands()
        {
            RestartGameCommand = new ReactiveCommand();
            FinishGameCommand = new ReactiveCommand();
        }
    }
}
