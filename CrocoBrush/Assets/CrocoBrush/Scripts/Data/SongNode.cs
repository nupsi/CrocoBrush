using System;

namespace CrocoBrush
{
    /// <summary>
    /// Stores Note data.
    /// </summary>
    [Serializable]
    public class SongNode
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Direction for the Node in the Mouth.
        /// </summary>
        public Direction Direction;

        /// <summary>
        /// Time for the Note in the music.
        /// </summary>
        public float Time;

        /// <summary>
        /// Delay between this and the previous Node.
        /// </summary>
        public float Delay;

        /*
         * Functions.
         */

        /// <summary>
        /// Create new Song Node with a direction and the time in the song.
        /// </summary>
        /// <param name="direction">Direction for the Note in the Mouth.</param>
        /// <param name="time">Time in the music.</param>
        public SongNode(Direction direction, float time)
        {
            this.Direction = direction;
            this.Time = time;
        }
    }
}