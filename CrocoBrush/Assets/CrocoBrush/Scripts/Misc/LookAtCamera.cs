using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Awake()
    {
        transform.LookAt(Camera.main.transform);
    }
}