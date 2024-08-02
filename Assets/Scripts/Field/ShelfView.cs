using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Products
{
    public class ShelfView : MonoBehaviour
    {

        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private ProductTypes _shelfType;

        public ProductTypes ShelfType => _shelfType;
        public Transform[] SpawnPoints => _spawnPoints;
    }
}