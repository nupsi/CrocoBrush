using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Contains Mouth's movement positions.
    /// Used to control positions for different levels.
    /// Call Select() to assign positions to bird.
    /// </summary>
    public class BirdPositions : MonoBehaviour
    {
        /*
         * Variables.
         */

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
            Bird.Instance.SetData(m_data);
        }
    }
}