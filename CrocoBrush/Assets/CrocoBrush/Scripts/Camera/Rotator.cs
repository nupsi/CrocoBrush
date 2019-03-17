using UnityEngine;

namespace CrocoBrush.Camera
{
    public class Rotator : MonoBehaviour
    {
        private void Update()
        {
            transform.Rotate(Vector3.up, 3f * Time.deltaTime);
        }
    }
}