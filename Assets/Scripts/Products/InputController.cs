using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Assets.Scripts.Products
{
    public class InputController : MonoBehaviour
    {
        public static event Action onMouseUp;
        public static event Action onMouseDown;

        public static bool BlockInput { get; set; }

        private void Update()
        {
            if (BlockInput)
            {
                return;
            }
            if (Input.GetMouseButtonDown(0)) {
                onMouseDown?.Invoke();
            }

            if (Input.GetMouseButtonUp(0))
            {
                onMouseUp?.Invoke();
            }
        }

    }
}