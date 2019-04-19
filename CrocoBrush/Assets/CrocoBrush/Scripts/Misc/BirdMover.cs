using UnityEngine;

namespace CrocoBrush
{
    public class BirdMover : MonoBehaviour
    {
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            var point = transform.position + transform.forward;
            var back = point - (transform.forward * 0.25f);
            Gizmos.DrawLine(transform.position, point);
            Gizmos.DrawLine(point, back + (transform.right * 0.25f));
            Gizmos.DrawLine(point, back - (transform.right * 0.25f));
        }

        private void OnEnable()
        {
#if UNITY_EDITOR
            if(UnityEditor.EditorApplication.isPlaying)
            {
#endif
                Bird.Instance?.MoveToPoint(transform, true, false, false);
#if UNITY_EDITOR
            }
#endif
        }
    }
}