using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Products
{
    public class ProductObject : MonoBehaviour
    {
        [SerializeField] private ProductView _productView;
        [SerializeField] private ProductTypes _productType;
        public ProductView ProductView => _productView;
        public Vector3 Postion => transform.position;
        public ProductTypes PoductType => _productType;

        public ProductPostion ProductPostion { get; set; }

        public void PlayScaleAnimation()
        {
            _productView.PlayScaleAnimation();
        }

        public void SetProductLayer()
        {
            _productView.SetProductLayer();
        }

        public void SetDragLayer()
        {
            _productView.SetDragLayer();
        }

        public Tween PlayReturnAnimation()
        {
            return transform.DOMove(ProductPostion.position, 0.2f).OnComplete(() => SetProductLayer());
        }

        public bool IsCorrectShelf()
        {
            return _productType == ProductPostion.shelfType;
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
    }
}