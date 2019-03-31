using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(MeshRenderer))]
    public class FoodIcon : MonoBehaviour
    {
        [SerializeField]
        private Material[] m_materials;

        private MeshRenderer m_renderer;

        private void Awake()
        {
            if(m_materials.Length == 0)
            {
                Debug.LogError("No materials to change!", this.gameObject);
                enabled = false;
                return;
            }

            m_renderer = GetComponent<MeshRenderer>();
        }

        private void OnEnable()
        {
            m_renderer.sharedMaterial = RandomMaterial;
        }

        private Material RandomMaterial => m_materials[Random.Range(0, m_materials.Length)];
    }
}