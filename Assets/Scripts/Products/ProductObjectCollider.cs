using UnityEngine;

namespace Assets.Scripts.Products
{
    public class ProductObjectCollider : MonoBehaviour
    {
        [SerializeField] private ProductObject _root;
        public ProductObject Root => _root;
    }
}