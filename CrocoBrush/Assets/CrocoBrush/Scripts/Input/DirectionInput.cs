using System;

namespace CrocoBrush
{
    /// <summary>
    /// Data structure to hold a keybind and a direction.
    /// </summary>
    [Serializable]
    public class DirectionInput
    {
        /// <summary>
        /// Keybind for the direction.
        /// </summary>
        public ScriptableKeybind Keybind;

        /// <summary>
        /// Direction for the keybind.
        /// </summary>
        public Direction Direction;
    }
}