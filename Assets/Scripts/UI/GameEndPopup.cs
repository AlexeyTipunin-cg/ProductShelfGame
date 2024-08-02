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
        private GameCommands _gameModel;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        [Inject]
        private void Init(GameCommands gameModel)
        {
            _gameModel = gameModel;
            gameModel.FinishGameCommand.Subscribe(_ => gameObject.SetActive(true)).AddTo(this);
            _restartButton.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            gameObject.SetActive(false);
            _gameModel.RestartGameCommand.Execute();
        }
    }
}