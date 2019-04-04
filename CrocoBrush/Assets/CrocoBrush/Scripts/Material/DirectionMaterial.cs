using System;
using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Stores material and direction.
    /// </summary>
    [Serializable]
    public struct DirectionMaterial
    {
        /// <summary>
        /// Current Direction.
        /// </summary>
        public Direction Direction;

        /// <summary>
        /// Current Material.
        /// </summary>
        public Material Material;
    }
}