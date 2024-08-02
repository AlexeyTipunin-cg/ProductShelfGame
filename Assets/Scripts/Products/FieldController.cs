using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Products
{
    public class FieldController : MonoBehaviour
    {
        private Dictionary<ProductTypes, List<ProductObject>> _field = new Dictionary<ProductTypes, List<ProductObject>>();
        private IProductFactory _productFactory;
        private RackView _rackView;

        [Inject]
        public void Init(IProductFactory productFactory, RackView rackView)
        {
            _productFactory = productFactory;
            _rackView = rackView;

            CreateField();
        }

        public void CreateField()
        {
            for (int i = 0; i < _rackView.Length; i++)
            {
                ShelfView shelf = _rackView.GetShelf(i);
                for (int j = 0; j < shelf.SpawnPoints.Length; j++)
                {
                    Vector3 pos = shelf.SpawnPoints[j].position;
                    ProductObject product = _productFactory.CreateProduct(shelf.ShelfType, pos, shelf.transform);
                    product.CurrentPos = pos;
                }
            }
        }
    }
}
