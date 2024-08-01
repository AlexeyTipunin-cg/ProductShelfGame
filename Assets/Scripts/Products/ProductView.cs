using DG.Tweening;
using System.Collections;
using UnityEngine;
using DG.Tweening.Core;
using System;

namespace Assets.Scripts.Products
{
    public class ProductView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private ProductObject _root;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Vector3 _initialOffset;
        private bool _isDragging = false;
        private static bool _playingAnimation = false;

        private float _bigScale = 1.1f;
        private float _animationTime = 0.1f;

        private Sequence _tween;

        public event Action OnProductDown;
        public ProductObject Root => _root;

        private void Awake()
        {
            //_collider.isTrigger = false;
        }

        //private void OnMouseDown()
        //{
        //    _isDragging = true;
        //    _initialOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    _collider.isTrigger = true;




        //    OnProductDown?.Invoke();


        //    StartCoroutine(DragMovement());
        //}

        public void PlayScaleAnimation()
        {
            if (_tween == null)
            {
                _tween = DOTween.Sequence().SetAutoKill(false);
                _tween.Insert(0, transform.DOScale(_bigScale, _animationTime));
                _tween.Insert(_animationTime, transform.DOScale(1, _animationTime));
            }
            else
            {
                transform.localScale = Vector3.one;
                _tween.Restart();
            }
        }

        public void SetDragLayer()
        {
            int id = SortingLayer.NameToID("DragLayer");
            _spriteRenderer.sortingLayerID = id;
        }

        public void SetProductLayer()
        {
            int id = SortingLayer.NameToID("Balls");
            _spriteRenderer.sortingLayerID = id;
        }

        //private void OnMouseUp()
        //{
        //    _isDragging = false;
        //    _collider.isTrigger = false;

        //    StopCoroutine(DragMovement());
        //}

        //private IEnumerator DragMovement()
        //{
        //    while (_isDragging)
        //    {
        //        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + _initialOffset;
        //        yield return null;
        //    }
        //}

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.gameObject.TryGetComponent(out ProductView productView))
        //    {

        //        if (_playingAnimation)
        //        {
        //            return;
        //        }
        //        _isDragging = false;
        //        _playingAnimation = true;
        //        _collider.isTrigger = false;



        //        Sequence seq = DOTween.Sequence();
        //        seq.Insert(0, productView.Root.transform.DOMove(Root.CurrentPos, 0.2f));
        //        seq.Insert(0, Root.transform.DOMove(productView.Root.CurrentPos, 0.2f));
        //        seq.OnComplete(() => _playingAnimation = false);

        //        Vector3 temp = productView.Root.CurrentPos;
        //        productView.Root.CurrentPos = Root.CurrentPos;
        //        Root.CurrentPos = temp;

        //        StopCoroutine(DragMovement());
        //    }


        //    Debug.Log("Trigger");
        //}



        private void OnDestroy()
        {
            if (_tween != null)
            {
                _tween.Kill();
            }
        }
    }
}