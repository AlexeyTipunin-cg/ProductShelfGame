using System;
using UniRx;


namespace Assets.Scripts.Products
{
    public class GameModel
    {
        public ReactiveCommand FinishGameCommand { get; }
        public ReactiveCommand RestartGameCommand { get;}
        public GameModel()
        {
            RestartGameCommand = new ReactiveCommand();
            FinishGameCommand = new ReactiveCommand();
        }
    }
}
