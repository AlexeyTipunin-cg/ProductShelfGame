using Assets.Scripts.Products;
using System;
using UniRx;

namespace Assets.Scripts.Audio
{
    public class SoundController : IDisposable
    {
        public const string GET_AUDIO = "get";
        public const string FINAL_AUDIO = "final";
        public const string WRONG_AUDIO = "wrong";

        private IGameCommands _gameCommands;
        private ISoundManager _soundManager;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        public SoundController(IGameCommands gameCommands, ISoundManager soundManager)
        {
            _gameCommands = gameCommands;
            _soundManager = soundManager;

            gameCommands.FinishGameCommand.Subscribe(_ => _soundManager.Play(FINAL_AUDIO)).AddTo(_compositeDisposable);
            gameCommands.OnWrongSwap.Subscribe(_ => _soundManager.Play(WRONG_AUDIO)).AddTo(_compositeDisposable);
            gameCommands.OnSuccsesfullSwap.Subscribe(_ => _soundManager.Play(GET_AUDIO)).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
            _compositeDisposable.Clear();
            _compositeDisposable = null;
        }
    }
}
