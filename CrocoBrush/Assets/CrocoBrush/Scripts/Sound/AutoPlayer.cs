using CrocoBrush;
using UnityEngine;

namespace Test.Sound
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
                    m_mout.Up();
                    break;
                case 1:
                    m_mout.Right();
                    break;
                case 2:
                    m_mout.Down();
                    break;
                case 3:
                    m_mout.Left();
                    count = -1;
                    break;
            }
            count++;
        }
    }
}