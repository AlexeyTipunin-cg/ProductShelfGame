using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Products
{
    public class ShelfView : MonoBehaviour
    {

        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private ProductObject _product;
        [SerializeField] private ProductTypes _shelfType;

        public ProductTypes ShelfType => _shelfType;
        public Transform[] SpawnPoints => _spawnPoints;

        //private void Awake()
        //{
        //    foreach (Transform transform in _spawnPoints)
        //    {
        //        ProductObject productView = Instantiate(_product, transform.position, Quaternion.identity, transform);
        //        productView.CurrentPos = transform.position;
        //    }
        //}
    }
}