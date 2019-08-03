using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Turns User input to Mouth Press Directions.
    /// </summary>
    public class MouthInput : MonoBehaviour
    {
        /// <summary>
        /// Input names and corresponding directions for those inputs.
        /// </summary>
        private Dictionary<string, Direction> m_inputs;

        /*
         * Mono Behaviour Functions.
         */

        protected void Awake()
        {
            //Create input dictionary.
            //key = input name (in Unity's input system)
            //value = input direction.
            m_inputs = new Dictionary<string, Direction>()
            {
                { "Up", Direction.Up },
                { "Down", Direction.Down },
                { "Left", Direction.Left },
                { "Right", Direction.Right }
            };
        }

        protected void Update()
        {
            //Go through all the inputs.
            //set.key = input name.
            //set.value = input direction.
            foreach(var set in m_inputs)
            {
                //If the current input is down.
                if(Input.GetButtonDown(set.Key))
                {
                    //Press to the direction of the current input.
                    Mouth.Instance.PressDirection(set.Value);
                }
            }
        }
    }
}