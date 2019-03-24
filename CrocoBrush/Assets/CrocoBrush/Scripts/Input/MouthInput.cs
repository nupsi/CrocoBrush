using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Turns User input to Mouth Press Directions.
    /// </summary>
    public class MouthInput : MonoBehaviour
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Key Code to press Down.
        /// </summary>
        private KeyCode m_down = KeyCode.DownArrow;

        /// <summary>
        /// Key Code to press Up.
        /// </summary>
        private KeyCode m_up = KeyCode.UpArrow;

        /// <summary>
        /// Key Code to press Left.
        /// </summary>
        private KeyCode m_left = KeyCode.LeftArrow;

        /// <summary>
        /// Key Code to press Right.
        /// </summary>
        private KeyCode m_right = KeyCode.RightArrow;

        /*
         * Mono Behaviour Functions.
         */

        private void Update()
        {
            if(Input.GetKeyDown(m_left))
            {
                Mouth.PressDirection(Direction.Left);
            }

            if(Input.GetKeyDown(m_right))
            {
                Mouth.PressDirection(Direction.Right);
            }

            if(Input.GetKeyDown(m_down))
            {
                Mouth.PressDirection(Direction.Down);
            }

            if(Input.GetKeyDown(m_up))
            {
                Mouth.PressDirection(Direction.Up);
            }
        }

        /*
         * Accessors.
         */

        /// <summary>
        /// Current Mouth Instance.
        /// </summary>
        /// <value>Current Mouth Instance.</value>
        private Mouth Mouth => Mouth.Instance;
    }
}