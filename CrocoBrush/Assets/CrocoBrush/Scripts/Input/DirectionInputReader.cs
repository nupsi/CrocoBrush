using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    public abstract class DirectionInputReader : MonoBehaviour
    {
        /// <summary>
        /// Input names and corresponding directions for those inputs.
        /// </summary>
        [SerializeField]
        private List<DirectionInput> m_input;

        protected virtual void Update()
        {
            UpdateInput();
        }

        /// <summary>
        /// Go through all the inputs added to the input list and call press direction if a key is down.
        /// </summary>
        protected virtual void UpdateInput()
        {
            //Loop through the direction inputs.
            //Go through all the inputs.
            foreach(var input in m_input)
            {
                //If the current input is down.
                if(input.Keybind.GetKeyDown())
                {
                    PressDirection(input.Direction);
                }
            }
        }

        protected abstract void PressDirection(Direction direction);
    }
}