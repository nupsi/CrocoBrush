using UnityEngine;

namespace CrocoBrush.Animation
{
    [RequireComponent(typeof(Renderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class MeshWave : MonoBehaviour
    {
        private Renderer m_renderer;
        private MeshFilter m_filter;
        private float m_offsetX = 0.01f;
        private float m_offsetY = 0.005f;
        private float m_scale = 0.3f;
        private float m_speed = 0.6f;
        private float m_noiseStrength = 1f;
        private float m_noiseWalk = 1f;

        private Vector3[] m_vertices;

        private void Awake()
        {
            m_filter = GetComponent<MeshFilter>();
            m_renderer = GetComponent<Renderer>();
            m_renderer.sharedMaterial.SetTextureOffset("_MainTex", Vector2.zero);
        }

        private void FixedUpdate()
        {
            var x = m_offsetX * Time.time;
            var y = m_offsetY * Time.time;
            m_renderer.sharedMaterial.SetTextureOffset("_MainTex", new Vector2(x, y));

            if(m_vertices == null)
            {
                m_vertices = m_filter.mesh.vertices;
            }

            var vertices = new Vector3[m_vertices.Length];
            for(int i = 0; i < vertices.Length; i++)
            {
                var vertex = m_vertices[i];
                vertex.y += Mathf.Sin(Time.time * m_speed + m_vertices[i].x + m_vertices[i].y + m_vertices[i].z) * m_scale;
                vertex.y += Mathf.PerlinNoise(m_vertices[i].x + m_noiseWalk, m_vertices[i].y + Mathf.Sin(Time.time * 0.1f)) * m_noiseStrength;
                vertices[i] = vertex;
            }

            m_filter.mesh.vertices = vertices;
        }
    }
}