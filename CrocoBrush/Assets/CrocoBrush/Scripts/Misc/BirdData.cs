using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Contains Position Data for Bird.
    /// </summary>
    public class BirdData
    {
        /*
         * Functions.
         */

        public BirdData(Transform up, Transform down, Transform left, Transform right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }

        /// <summary>
        /// Get Transform for given direction.
        /// </summary>
        /// <param name="direction">Direction where the transform is.</param>
        /// <returns>Transform in given direction.</returns>
        public Transform GetDirection(Direction direction)
        {
            switch(direction)
            {
                case Direction.Up:
                    return Up;

                case Direction.Down:
                    return Down;

                case Direction.Right:
                    return Right;

                case Direction.Left:
                    return Left;
            }
            return null;
        }

        /*
         * Accessors.
         */

        /// <summary>
        /// Up position in mouth.
        /// </summary>
        public Transform Up { get; }

        /// <summary>
        /// Down position in mouth.
        /// </summary>
        public Transform Down { get; }

        /// <summary>
        /// Left position in mouth.
        /// </summary>
        public Transform Left { get; }

        /// <summary>
        /// Right poistion in mouth.
        /// </summary>
        public Transform Right { get; }
    }
}