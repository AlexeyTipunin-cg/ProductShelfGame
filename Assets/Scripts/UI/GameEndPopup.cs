using Assets.Scripts.Products;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI
{
    public class GameEndPopup : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        private IGameCommands _gameCommands;

        [Inject]
        private void Init(IGameCommands gameCommands)
        {
            _gameCommands = gameCommands;
            _restartButton.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            _gameCommands.RestartGameCommand.Execute();
        }
    }
}