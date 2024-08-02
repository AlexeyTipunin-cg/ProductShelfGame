using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Products
{
    public class ProductObject : MonoBehaviour
    {
        [SerializeField]private ProductView _productView;
        [SerializeField]private ProductTypes _productType;

        public Vector3 CurrentPos { get; set; }
        public ProductView ProductView => _productView;

        public Vector3 Postion => transform.position;

        public ProductTypes PoductType => _productType;

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
            return transform.DOMove(CurrentPos, 0.2f).OnComplete(() => SetProductLayer());
        }
    }
}