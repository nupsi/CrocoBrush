using System;

namespace CrocoBrush
{
    [Serializable]
    public class SongNode
    {
        public Direction Direction;
        public float Time;
        public float Delay;

        public SongNode(Direction direction, float time)
        {
            this.Direction = direction;
            this.Time = time;
        }
    }
}