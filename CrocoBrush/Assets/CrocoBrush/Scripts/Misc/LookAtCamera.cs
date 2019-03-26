using UnityEngine;

namespace CrocoBrush
{
    public class LookAtCamera : MonoBehaviour
    {
        private void Awake()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}