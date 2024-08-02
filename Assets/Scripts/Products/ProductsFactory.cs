using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Products
{
    public class ProductsFactory : MonoBehaviour, IProductFactory
    {

        [SerializeField] private ProductObject[] _productObject;

        public ProductObject CreateProduct(ProductTypes type, Vector3 position, Transform parent)
        {
            ProductObject prefab = _productObject.First(p => p.PoductType == type);
            return Instantiate(prefab, position, Quaternion.identity, parent) as ProductObject;
        }

    }
}