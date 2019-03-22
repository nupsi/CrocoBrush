using UnityEngine;

namespace CrocoBrush
{
    public class Bird : MonoBehaviour
    {
        public GameObject m_bottom;
        public GameObject m_top;
        public GameObject m_left;
        public GameObject m_right;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position = m_left.transform.position;
            }

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position = m_right.transform.position;
            }

            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.position = m_bottom.transform.position;
            }

            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.position = m_top.transform.position;
            }
        }
    }
}