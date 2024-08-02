using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Products
{
    public class FieldController : MonoBehaviour
    {
        private Dictionary<ProductTypes, List<ProductObject>> _typesToProducts = new Dictionary<ProductTypes, List<ProductObject>>();
        private Dictionary<ProductTypes, Vector3[]> _productPosOnShelf = new Dictionary<ProductTypes, Vector3[]>();
        private List<ProductPostion> _productPositions = new List<ProductPostion>();
        private List<ProductObject> _allProductViews;

        private IProductFactory _productFactory;
        private RackView _rackView;
        private IGameCommands _gameModel;

        private CompositeDisposable _compositeDisposable;


        [Inject]
        public void Init(IProductFactory productFactory, IGameCommands gameModel, RackView rackView)
        {
            _productFactory = productFactory;
            _rackView = rackView;
            _gameModel = gameModel;

            for (int i = 0; i < _rackView.Length; i++)
            {
                ShelfView shelf = _rackView.GetShelf(i);
                Vector3[] positions = shelf.SpawnPoints.Select(tr => tr.position).ToArray();
                _productPosOnShelf[shelf.ShelfType] = positions;
                foreach (var p in positions)
                {
                    _productPositions.Add(new ProductPostion { shelfType = shelf.ShelfType, position = p });
                }
            }

            CreateAndResuffle();
            _gameModel.RestartGameCommand.Subscribe(_ => OnGameRestart()).AddTo(this);
            _gameModel.OnSwapEnd.Subscribe(_ => CheckGameState()).AddTo(this);
        }

        private void CheckGameState()
        {
            if (CheckIfEndGame())
            {
                Observable.Timer(TimeSpan.FromSeconds(0.5)).Subscribe(
                    _ => CallFinishGameCommand());
            }
            else
            {
                InputController.BlockInput = false;
            }

        }

        private void CallFinishGameCommand()
        {
            _gameModel.FinishGameCommand.Execute();
        }

        private void OnGameRestart()
        {
            _compositeDisposable.Dispose();
            _compositeDisposable.Clear();
            _compositeDisposable = null;

            CreateAndResuffle();
        }

        public bool CheckIfEndGame()
        {
            bool allCorrect = true;
            foreach (ProductObject product in _allProductViews)
            {
                if (!product.IsCorrectShelf())
                {
                    allCorrect = false;
                    break;
                }
            }
            return allCorrect;
        }

        public void CreateAndResuffle()
        {
            InputController.BlockInput = true;
            _compositeDisposable = new CompositeDisposable();
            CreateCorrectField();
            StartCoroutine(Reshuffle());
        }

        public void CreateCorrectField()
        {
            if (_allProductViews == null)
            {
                _allProductViews = new List<ProductObject>();
                for (int i = 0; i < _rackView.Length; i++)
                {
                    ShelfView shelf = _rackView.GetShelf(i);
                    _typesToProducts[shelf.ShelfType] = new List<ProductObject>();
                    foreach (Vector3 pos in _productPosOnShelf[shelf.ShelfType])
                    {
                        ProductObject product = _productFactory.CreateProduct(shelf.ShelfType, pos, transform);
                        _typesToProducts[shelf.ShelfType].Add(product);
                    }
                }

                _allProductViews = _typesToProducts.Values.SelectMany(x => x).ToList();
            }
            else
            {
                for (int i = 0; i < _rackView.Length; i++)
                {
                    ShelfView shelf = _rackView.GetShelf(i);
                    for (int j = 0; j < _productPosOnShelf[shelf.ShelfType].Length; j++)
                    {
                        Vector3 pos = _productPosOnShelf[shelf.ShelfType][j];
                        _typesToProducts[shelf.ShelfType][j].transform.position = pos;
                    }
                }
            }

            IList<ProductPostion> shuffled = _productPositions.Shuffle();
            if (_allProductViews.Count == shuffled.Count)
            {
                for (int i = 0; i < shuffled.Count; i++)
                {
                    _allProductViews[i].ProductPostion = shuffled[i];
                }
            }
        }

        public IEnumerator Reshuffle()
        {
            yield return new WaitForSeconds(2f);

            Sequence seq = DOTween.Sequence();

            foreach (ProductObject view in _allProductViews)
            {
                seq.Insert(0.1f, view.PlayReturnAnimation());
            }

            seq.OnComplete(() => InputController.BlockInput = false);
        }
    }
}
