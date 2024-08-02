using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Products
{
    public class ProductObject : MonoBehaviour
    {
        [SerializeField] private ProductObjectCollider _productView;
        [SerializeField] private ProductTypes _productType;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Sequence _tween;
        private Vector3 _initialOffset;
        private float _bigScale = 1.1f;
        private float _animationTime = 0.1f;
        public ProductObjectCollider ProductView => _productView;
        public Vector3 Postion => transform.position;
        public ProductTypes PoductType => _productType;
        public ProductPostion ProductPostion { get; set; }

        public Tween PlayReturnAnimation()
        {
            return transform.DOMove(ProductPostion.position, 0.2f).OnComplete(() => SetProductLayer());
        }

        public bool IsCorrectShelf()
        {
            return _productType == ProductPostion.shelfType;
        }

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

        public Tween PlayWrongAnimation()
        {
            float time = 0.1f;

            Sequence seq = DOTween.Sequence();
            seq.Insert(0, transform.DOLocalRotate(Vector3.forward * -15, time));
            seq.Insert(time, transform.DOLocalRotate(Vector3.forward * 15, time * 2));
            seq.Insert(time * 3, transform.DOLocalRotate(Vector3.zero, time));
            seq.Insert(time * 4, PlayReturnAnimation());
            return seq;
        }

        public void SetDragLayer()
        {
            SetSpriteLayer(SpriteLayers.DragLayer.ToString());

            _spriteRenderer.gameObject.layer = (int)GameLayers.Drag;
        }

        public void SetProductLayer()
        {
            SetSpriteLayer(SpriteLayers.Products.ToString());

            _spriteRenderer.gameObject.layer = (int)GameLayers.Products;

        }

        private void SetSpriteLayer(string name)
        {
            int id = SortingLayer.NameToID(name);
            _spriteRenderer.sortingLayerID = id;
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