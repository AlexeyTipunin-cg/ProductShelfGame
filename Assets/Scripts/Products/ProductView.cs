using DG.Tweening;
using System.Collections;
using UnityEngine;
using DG.Tweening.Core;

namespace Assets.Scripts.Products
{
    public class ProductView : MonoBehaviour
    {
        private Vector3 _initialOffset;
        private bool _isDragging = false;

        private float _bigScale = 1.1f;
        private float _animationTime = 0.1f;

        private Sequence _tween;

        private void OnMouseDown()
        {
            _isDragging = true;
            _initialOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
            

            StartCoroutine(DragMovement());
        }

        private void OnMouseUp()
        {
            _isDragging = false;
            StopCoroutine(DragMovement());
        }

        private IEnumerator DragMovement()
        {
            while (_isDragging)
            {
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + _initialOffset;
                yield return null;
            }
        }

        private void OnDestroy()
        {
            if ( _tween != null)
            {
                _tween.Kill();
            }
        }
    }
}