using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Products
{
    public class FieldView : MonoBehaviour, IFieldView
    {
        private bool _isDragging;
        private bool _canSwap;
        private bool _isWrongSwap;

        private Vector3 _initialOffset;
        private IGameCommands _gameCommands;

        private ProductObject _currentSelected;
        private ProductObject _intersectedObject;


        private void Awake()
        {
            InputController.onMouseUp += OnMouseUpInternal;
            InputController.onMouseDown += OnMouseDownInternal;
        }

        [Inject]
        public void Init(IGameCommands commands)
        {
            _gameCommands = commands;
        }
        private void OnMouseDownInternal()
        {
            _canSwap = false;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D[] results = new RaycastHit2D[1];
            int count = Physics2D.RaycastNonAlloc(mousePos, Camera.main.transform.forward, results, 30, 1 << (int)GameLayers.Products);
            if (count > 0)
            {
                if (results[0].collider.TryGetComponent(out ProductObjectCollider productView))
                {
                    _currentSelected = productView.Root;
                    if (productView.Root.IsCorrectShelf())
                    {
                        return;
                    }

                    _isDragging = true;
                    _initialOffset = _currentSelected.Postion - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    _intersectedObject = null;
                    _currentSelected.SetDragLayer();
                    _currentSelected.PlayScaleAnimation();

                    StartCoroutine(DragMovement(_currentSelected));
                }
            }
        }

        private IEnumerator DragMovement(ProductObject productView)
        {
            while (_isDragging)
            {
                productView.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + _initialOffset;
                RaycastHit2D[] results = new RaycastHit2D[1];

                int count = Physics2D.RaycastNonAlloc(productView.transform.position, Camera.main.transform.forward, results, 5, 1 << (int)GameLayers.Products);

                _intersectedObject = null;
                _canSwap = false;
                _isWrongSwap = true;

                if (count > 0)
                {
                    if (results[0].collider.TryGetComponent(out ProductObjectCollider productViewCollided))
                    {
                        if (!productViewCollided.Root.IsCorrectShelf())
                        {
                            if (productView.PoductType == productViewCollided.Root.ProductPostion.shelfType)
                            {
                                _intersectedObject = productViewCollided.Root;
                                _canSwap = true;
                                _isWrongSwap = false;
                            }
                        }
                    }

                }
                else
                {
                    _isWrongSwap = false;
                }

                yield return null;
            }
        }

        private void OnMouseUpInternal()
        {
            _isDragging = false;
            StopAllCoroutines();

            if (_currentSelected != null)
            {

                InputController.BlockInput = true;
                Sequence seq = DOTween.Sequence();

                if (_canSwap)
                {
                    ProductPostion temp = _intersectedObject.ProductPostion;
                    _intersectedObject.ProductPostion = _currentSelected.ProductPostion;
                    _currentSelected.ProductPostion = temp;


                    _intersectedObject.SetDragLayer();
                    seq.Insert(0, _currentSelected.PlayReturnAnimation());
                    seq.Insert(0, _intersectedObject.PlayReturnAnimation());

                    _gameCommands.OnSuccsesfullSwap.Execute();

                }
                else
                {
                    if (_isWrongSwap)
                    {
                        seq.Insert(0, _currentSelected.PlayWrongAnimation());
                        _gameCommands.OnWrongSwap.Execute();
                    }
                    else
                    {
                        seq.Insert(0, _currentSelected.PlayReturnAnimation());
                    }

                    seq.OnComplete(() =>
                    {
                        _gameCommands.OnWrongSwap.Execute();
                    });

                }

                seq.OnComplete(() =>
                {
                    _gameCommands.OnSwapEnd.Execute();
                });

                _currentSelected = null;
                _intersectedObject = null;
                _canSwap = false;
                _isWrongSwap = false;
            }
        }

        private void OnDestroy()
        {
            InputController.onMouseUp -= OnMouseUpInternal;
            InputController.onMouseDown -= OnMouseDownInternal;
        }
    }
}