using UnityEngine;

namespace CrocoBrush
{
    public class BirdPositions : MonoBehaviour
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Target bird;
        /// </summary>
        [SerializeField]
        private Bird m_bird;

        /// <summary>
        /// Position for the up position in the mouth.
        /// </summary>
        [SerializeField]
        private GameObject m_up;

        /// <summary>
        /// Position for the down position in the mouth.
        /// </summary>
        [SerializeField]
        private GameObject m_down;

        /// <summary>
        /// Position for the left position in the mouth.
        /// </summary>
        [SerializeField]
        private GameObject m_left;

        /// <summary>
        /// Position for the right position in the mouth.
        /// </summary>
        [SerializeField]
        private GameObject m_right;

        /// <summary>
        /// This position as bird data.
        /// </summary>
        private BirdData m_data;

        /*
         * Mono Behaviour Functions.
         */

        private void Awake()
        {
            m_data = new BirdData(m_up.transform, m_down.transform, m_left.transform, m_right.transform);
        }

        /*
         * Functions.
         */

        /// <summary>
        /// Set the position data for the target bird.
        /// </summary>
        public void Select()
        {
            m_bird.SetData(m_data);
        }
    }
}