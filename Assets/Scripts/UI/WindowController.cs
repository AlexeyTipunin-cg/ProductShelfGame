using Assets.Scripts.Products;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class WindowController : MonoBehaviour
    {
        [SerializeField] private GameEndPopup _gameEndPopup;

        [Inject]
        private void Init(IGameCommands gameCommands)
        {
            gameCommands.FinishGameCommand.Subscribe(_ => ShowGameEndWindow()).AddTo(this);
            gameCommands.RestartGameCommand.Subscribe(_ => HideGameEndWindow()).AddTo(this);
        }

        private void ShowGameEndWindow()
        {
            _gameEndPopup.gameObject.SetActive(true);
        }

        private void HideGameEndWindow()
        {
            _gameEndPopup.gameObject.SetActive(false);
        }
    }
}
