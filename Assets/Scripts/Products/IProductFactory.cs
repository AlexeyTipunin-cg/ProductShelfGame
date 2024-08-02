using UnityEngine;

namespace Assets.Scripts.Products
{
    public interface IProductFactory
    {
        ProductObject CreateProduct(ProductTypes type, Vector3 position, Transform parent);
    }
}