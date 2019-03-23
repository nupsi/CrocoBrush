using UnityEngine;

namespace CrocoBrush
{
    public class MouthInput : MonoBehaviour
    {
        private KeyCode m_down = KeyCode.DownArrow;
        private KeyCode m_up = KeyCode.UpArrow;
        private KeyCode m_left = KeyCode.LeftArrow;
        private KeyCode m_right = KeyCode.RightArrow;

        private void Update()
        {
            if(Input.GetKeyDown(m_left))
            {
                Mouth.PressDirection(Direction.Left);
            }

            if(Input.GetKeyDown(m_right))
            {
                Mouth.PressDirection(Direction.Right);
            }

            if(Input.GetKeyDown(m_down))
            {
                Mouth.PressDirection(Direction.Down);
            }

            if(Input.GetKeyDown(m_up))
            {
                Mouth.PressDirection(Direction.Up);
            }
        }

        private Mouth Mouth => Mouth.Instance;
    }
}