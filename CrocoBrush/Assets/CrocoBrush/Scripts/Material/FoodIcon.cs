using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Changes to random material when the game object is activated.
    /// </summary>
    [RequireComponent(typeof(MeshRenderer))]
    public class FoodIcon : MonoBehaviour
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Array of possible materials.
        /// The material is randomly selected from these materials.
        /// </summary>
        [SerializeField]
        private Material[] m_materials;

        /// <summary>
        /// Target renderer for changing the material.
        /// </summary>
        private MeshRenderer m_renderer;

        /*
         * Mono Behaviour Functions.
         */

        private void Awake()
        {
            if(m_materials.Length == 0)
            {
                Debug.LogError("No materials to change!", this.gameObject);
                enabled = false;
                return;
            }
            //Get the current mesh renderer component to be the target renderer.
            m_renderer = GetComponent<MeshRenderer>();
        }

        private void OnEnable()
        {
            //Change the shared material (to preven instantiated materials) to a random material.
            m_renderer.sharedMaterial = RandomMaterial;
        }

        /*
         * Accessors.
         */

        /// <summary>
        /// Returns a random material from the current material array.
        /// </summary>
        /// <value>A Random material from the material array.</value>
        private Material RandomMaterial => m_materials[Random.Range(0, m_materials.Length)];
    }
}