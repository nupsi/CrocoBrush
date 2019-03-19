using UnityEngine;

namespace CrocoBrush
{
    public class Food : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_circle;

        private Mouth m_mouth;
        private float m_time;
        private bool m_initialized;

        public void Initialize(Mouth mouth, Direction direction, float time, Vector3 position)
        {
            m_mouth = mouth;
            m_time = time;
            transform.position = position;
            m_circle.transform.localScale = Vector3.one * 2;
            m_initialized = true;
            transform.LookAt(UnityEngine.Camera.main.transform);
        }

        private void Update()
        {
            if(m_initialized)
            {
                if (m_time > 0)
                {
                    if(Circle.x > 1)
                    {
                        m_circle.transform.localScale -= Vector3.one * Time.deltaTime;
                    }
                    m_time -= Time.deltaTime;
                }
                else 
                {
                    m_mouth.Remove(this);
                }
            }
        }

        private Vector3 Circle 
        {
            get => m_circle.transform.localScale; 
            set => m_circle.transform.localScale = value; 
        }

        public Direction Direction { get; }
    }
}