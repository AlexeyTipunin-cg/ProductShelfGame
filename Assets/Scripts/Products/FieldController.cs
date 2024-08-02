using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Products
{
    public class FieldController : MonoBehaviour
    {
        private Dictionary<ProductTypes, List<ProductObject>> _typesToProducts = new Dictionary<ProductTypes, List<ProductObject>>();
        private Dictionary<ProductTypes, Vector3[]> _fieldsPositions = new Dictionary<ProductTypes, Vector3[]>();
        private List<ProductPostion> _productPositions = new List<ProductPostion>();
        private List<ProductObject> allProducts;
        private IProductFactory _productFactory;
        private RackView _rackView;
        private GameModel _gameModel;


        [Inject]
        public void Init(IProductFactory productFactory, GameModel gameModel,  RackView rackView)
        {
            _productFactory = productFactory;
            _rackView = rackView;
            _gameModel = gameModel;

            for (int i = 0; i < _rackView.Length; i++)
            {
                ShelfView shelf = _rackView.GetShelf(i);
                Vector3[] positions = shelf.SpawnPoints.Select(tr => tr.position).ToArray();
                _fieldsPositions[shelf.ShelfType] = positions;
                foreach (var p in positions)
                {
                    _productPositions.Add(new ProductPostion { shelfType = shelf.ShelfType, position = p });
                }

            }

            CreateAndResuffle();
            _gameModel.RestartGameCommand.Subscribe(() => CreateAndResuffle()).AddTo(this);

            rackView.GetFiledView().onSwapEnd += CheckGameState;
        }

        private void CheckGameState()
        {
            if (CheckIfEndGame())
            {
                _gameModel.FinishGameCommand.Execute();
            }
            else
            {
                InputController.BlockInput = false;
            }

        }

        public bool CheckIfEndGame()
        {
            bool allCorrect = true;
            foreach (ProductObject product in allProducts)
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
            CreateCorrectField();
            StartCoroutine(Reshuffle());
        }

        public void CreateCorrectField()
        {
            if (allProducts == null)
            {
                allProducts = new List<ProductObject>();
                for (int i = 0; i < _rackView.Length; i++)
                {
                    ShelfView shelf = _rackView.GetShelf(i);
                    _typesToProducts[shelf.ShelfType] = new List<ProductObject>();
                    foreach (Vector3 pos in _fieldsPositions[shelf.ShelfType])
                    {
                        ProductObject product = _productFactory.CreateProduct(shelf.ShelfType, pos, transform);
                        _typesToProducts[shelf.ShelfType].Add(product);
                    }
                }

                allProducts = _typesToProducts.Values.SelectMany(x => x).ToList();
            }
            else
            {
                for (int i = 0; i < _rackView.Length; i++)
                {
                    ShelfView shelf = _rackView.GetShelf(i);
                    for (int j = 0; j < _fieldsPositions[shelf.ShelfType].Length; j++)
                    {
                        Vector3 pos = _fieldsPositions[shelf.ShelfType][j];
                        _typesToProducts[shelf.ShelfType][j].transform.position = pos;
                    }
                }
            }

            IList<ProductPostion> shuffled = _productPositions.Shuffle();
            if (allProducts.Count == shuffled.Count)
            {
                for (int i = 0; i < shuffled.Count; i++)
                {
                    allProducts[i].ProductPostion = shuffled[i];
                }
            }
        }

        public IEnumerator Reshuffle()
        {
            yield return new WaitForSeconds(2f);

            Sequence seq = DOTween.Sequence();

            foreach (ProductObject view in allProducts)
            {
                seq.Insert(0.1f, view.PlayReturnAnimation());
            }

            seq.OnComplete(() => InputController.BlockInput = false);
        }

        private void OnDestroy()
        {
            _rackView.GetFiledView().onSwapEnd -= CheckGameState;
        }
    }
}
