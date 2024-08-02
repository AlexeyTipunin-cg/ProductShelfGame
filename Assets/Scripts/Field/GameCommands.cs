using System;
using UniRx;
using Unity.VisualScripting;


namespace Assets.Scripts.Products
{
    public class GameCommands: IGameCommands
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
