using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Turns User input to Mouth Press Directions.
    /// </summary>
    public class MouthInput : MonoBehaviour
    {
        private Dictionary<string, Direction> m_inputs;

        /*
         * Mono Behaviour Functions.
         */

        private void Awake()
        {
            m_inputs = new Dictionary<string, Direction>()
            {
                { "Up", Direction.Up },
                { "Down", Direction.Down },
                { "Left", Direction.Left },
                { "Right", Direction.Right }
            };
        }

        private void Update()
        {
            foreach(var set in m_inputs)
            {
                if(Input.GetButtonDown(set.Key))
                {
                    Mouth.Instance.PressDirection(set.Value);
                }
            }
        }
    }
}