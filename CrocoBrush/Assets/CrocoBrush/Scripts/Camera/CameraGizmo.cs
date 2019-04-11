using UnityEngine;

public class CameraGizmo : MonoBehaviour
{
#if UNITY_EDITOR
    private Color m_color = Color.blue;
    private float m_distance = 0.5f;
    private float m_size = 0.5f;

    private void OnDrawGizmos()
    {
        var center = transform.position;
        var offset = center + transform.forward * m_distance;
        var corner_1 = offset + transform.up * (m_size / 2) + transform.right * m_size;
        var corner_2 = offset - transform.up * (m_size / 2) + transform.right * m_size;
        var corner_3 = offset + transform.up * (m_size / 2) - transform.right * m_size;
        var corner_4 = offset - transform.up * (m_size / 2) - transform.right * m_size;
        var top_1 = corner_3 + transform.right * (m_size / 2);
        var top_2 = top_1 + transform.right * (m_size / 2) + transform.up * (m_size / 4);
        var top_3 = top_1 + transform.right * m_size;

        Gizmos.color = m_color;
        Gizmos.DrawLine(center, corner_1);
        Gizmos.DrawLine(center, corner_2);
        Gizmos.DrawLine(center, corner_3);
        Gizmos.DrawLine(center, corner_4);
        Gizmos.DrawLine(corner_1, corner_2);
        Gizmos.DrawLine(corner_3, corner_4);
        Gizmos.DrawLine(corner_1, corner_3);
        Gizmos.DrawLine(corner_2, corner_4);
        Gizmos.DrawLine(top_1, top_2);
        Gizmos.DrawLine(top_2, top_3);
    }

#endif
}