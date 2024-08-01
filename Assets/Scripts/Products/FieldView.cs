using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Products
{
    public class FieldView : MonoBehaviour
    {
        private bool _isDragging;
        private bool _canSwap;
        private bool _playFlyAnimation;
        private Vector3 _initialOffset;

        private ProductObject _currentSelected;
        private ProductObject _intersectedObject;

        private void Awake()
        {
            InputController.onMouseUp += OnMouseUpInternal;
            InputController.onMouseDown += OnMouseDownInternal;
        }

        private void OnMouseDownInternal()
        {
            _canSwap = false;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D[] results = new RaycastHit2D[1];
            int count = Physics2D.RaycastNonAlloc(mousePos, Camera.main.transform.forward, results, 30, 1 << (int)GameLayers.Products);
            if (count > 0)
            {
                if (results[0].collider.TryGetComponent(out ProductView productView))
                {

                    _isDragging = true;
                    _initialOffset = productView.Root.Postion - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    _currentSelected = productView.Root;
                    _currentSelected.SetDragLayer();
                    productView.PlayScaleAnimation();

                    StartCoroutine(DragMovement(productView));
                }
            }
        }

        private IEnumerator DragMovement(ProductView productView)
        {
            while (_isDragging)
            {
                productView.Root.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + _initialOffset;
                RaycastHit2D[] results = new RaycastHit2D[2];

                int count = Physics2D.RaycastNonAlloc(productView.Root.transform.position, Camera.main.transform.forward, results, 30, 1 << (int)GameLayers.Products);
                if (count == 2)
                {
                    for (int i = 0; i < results.Length; i++)
                    {

                        if (results[i].collider.TryGetComponent(out ProductView productViewCollided))
                        {
                            if (productViewCollided.Root != _currentSelected)
                            {
                                _intersectedObject = productViewCollided.Root;
                                _canSwap = true;
                                break;
                            }
                        }
                    }

                }
                yield return null;
            }
        }

        private void OnMouseUpInternal()
        {
            _isDragging = false;
            if (_currentSelected != null)
            {

                if (_canSwap)
                {
                    Vector3 temp = _intersectedObject.CurrentPos;
                    _intersectedObject.CurrentPos = _currentSelected.CurrentPos;
                    _currentSelected.CurrentPos = temp;

                    Sequence seq = DOTween.Sequence();
                    _intersectedObject.SetDragLayer();
                    seq.Insert(0, _currentSelected.PlayReturnAnimation());
                    seq.Insert(0, _intersectedObject.PlayReturnAnimation());
                    seq.OnComplete(() => _playFlyAnimation = false);


                }
                else
                {
                    _currentSelected.PlayReturnAnimation();
                    _currentSelected = null;
                    _intersectedObject = null;
                    _canSwap = false;
                }
            }
        }

        private void OnDestroy()
        {
            InputController.onMouseUp -= OnMouseUpInternal;
            InputController.onMouseDown -= OnMouseDownInternal;
        }
    }
}