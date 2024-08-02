using DG.Tweening;
using System.Collections;
using UnityEngine;
using DG.Tweening.Core;
using System;

namespace Assets.Scripts.Products
{
    public class ProductView : MonoBehaviour
    {
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

            gameObject.layer = (int)GameLayers.Drag;
        }

        public void SetProductLayer()
        {
            int id = SortingLayer.NameToID("Balls");
            _spriteRenderer.sortingLayerID = id;

            gameObject.layer = (int)GameLayers.Products;

        }

        private void OnDestroy()
        {
            if (_tween != null)
            {
                _tween.Kill();
            }
        }
    }
}