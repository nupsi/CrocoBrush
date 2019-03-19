using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(Mouth))]
    public class MouthInput : MonoBehaviour
    {
        private Mouth m_mouth;
        private KeyCode m_down = KeyCode.DownArrow;
        private KeyCode m_up = KeyCode.UpArrow;
        private KeyCode m_left = KeyCode.LeftArrow;
        private KeyCode m_right = KeyCode.RightArrow;

        private void Awake()
        {
            m_mouth = GetComponent<Mouth>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(m_left))
            {
                m_mouth.PressDirection(Direction.Left);
            }

            if(Input.GetKeyDown(m_right))
            {
                m_mouth.PressDirection(Direction.Right);
            }

            if(Input.GetKeyDown(m_down))
            {
                m_mouth.PressDirection(Direction.Down);
            }

            if(Input.GetKeyDown(m_up))
            {
                m_mouth.PressDirection(Direction.Up);
            }
        }
    }
}