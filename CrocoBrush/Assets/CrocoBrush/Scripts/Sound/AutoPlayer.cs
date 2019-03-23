using CrocoBrush;
using UnityEngine;

namespace CrocoBrush.Sound
{
    public class AutoPlayer : Analyzer
    {
        [SerializeField]
        private Mouth m_mout;
        private int count = 0;

        protected override void RequestInput()
        {
            switch(count)
            {
                case 0:
                    m_mout.Create(Direction.Up);
                    break;
                case 1:
                    m_mout.Create(Direction.Right);
                    break;
                case 2:
                    m_mout.Create(Direction.Down);
                    break;
                case 3:
                    m_mout.Create(Direction.Left);
                    count = -1;
                    break;
            }
            count++;
        }
    }
}