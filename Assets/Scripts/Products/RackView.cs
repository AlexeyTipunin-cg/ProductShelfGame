using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Products
{
    public class RackView : MonoBehaviour
    {
        [SerializeField]private ShelfView[] _shelfView;
        [SerializeField]private FieldView _fieldView;

        public int Length => _shelfView.Length;

        public ShelfView GetShelf(int index)
        {
            return _shelfView[index];
        }
        
        public IFieldView GetFiledView()
        {
            return _fieldView;
        }
    }
}