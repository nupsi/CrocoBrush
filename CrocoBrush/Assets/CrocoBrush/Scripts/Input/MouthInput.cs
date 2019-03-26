using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Turns User input to Mouth Press Directions.
    /// </summary>
    public class MouthInput : MonoBehaviour
    {
        /*
         * Mono Behaviour Functions.
         */

        private void Update()
        {
            if(Input.GetButtonDown("Left"))
            {
                Mouth.PressDirection(Direction.Left);
            }

            if(Input.GetButtonDown("Right"))
            {
                Mouth.PressDirection(Direction.Right);
            }

            if(Input.GetButtonDown("Down"))
            {
                Mouth.PressDirection(Direction.Down);
            }

            if(Input.GetButtonDown("Up"))
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