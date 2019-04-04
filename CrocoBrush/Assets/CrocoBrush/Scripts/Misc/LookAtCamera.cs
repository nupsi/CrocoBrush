using System;
using UnityEngine;

namespace CrocoBrush
{
    [Obsolete("This class should not be required. See Food for the latest look at solution.", false)]
    public class LookAtCamera : MonoBehaviour
    {
        private void Awake()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}