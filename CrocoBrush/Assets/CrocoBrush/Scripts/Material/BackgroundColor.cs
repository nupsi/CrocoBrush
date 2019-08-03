using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(MeshRenderer))]
    public class BackgroundColor : MonoBehaviour
    {
        [SerializeField]
        private DirectionMaterial[] m_materials;

        private MeshRenderer m_renderer;

        protected void Awake()
        {
            m_renderer = GetComponent<MeshRenderer>();
        }

        public void UpdateMaterial(Direction direction)
        {
            m_renderer.sharedMaterial = GetMaterial(direction);
        }

        private Material GetMaterial(Direction direction)
        {
            foreach(var material in m_materials)
            {
                if(material.Direction == direction)
                {
                    return material.Material;
                }
            }
            return m_renderer.sharedMaterial;
        }
    }
}