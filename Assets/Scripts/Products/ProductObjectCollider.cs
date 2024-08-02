using DG.Tweening;
using System.Collections;
using UnityEngine;
using DG.Tweening.Core;
using System;

namespace Assets.Scripts.Products
{
    public class ProductObjectCollider : MonoBehaviour
    {
        [SerializeField] private ProductObject _root;
        public ProductObject Root => _root;

        //public void PlayScaleAnimation()
        //{
        //    if (_tween == null)
        //    {
        //        _tween = DOTween.Sequence().SetAutoKill(false);
        //        _tween.Insert(0, transform.DOScale(_bigScale, _animationTime));
        //        _tween.Insert(_animationTime, transform.DOScale(1, _animationTime));
        //    }
        //    else
        //    {
        //        transform.localScale = Vector3.one;
        //        _tween.Restart();
        //    }
        //}

        //public void SetDragLayer()
        //{
        //    SetSpriteLayer(SpriteLayers.DragLayer.ToString());

        //    gameObject.layer = (int)GameLayers.Drag;
        //}

        //public void SetProductLayer()
        //{
        //    SetSpriteLayer(SpriteLayers.Products.ToString());

        //    gameObject.layer = (int)GameLayers.Products;

        //}

        //private void SetSpriteLayer(string name)
        //{
        //    int id = SortingLayer.NameToID(name);
        //    _spriteRenderer.sortingLayerID = id;
        //}


    }
}