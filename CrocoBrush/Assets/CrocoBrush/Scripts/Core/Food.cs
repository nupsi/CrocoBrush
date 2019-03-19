using UnityEngine;

namespace CrocoBrush
{
    public class Food : MonoBehaviour
    {
        private GameObject m_circle;
        private Teeth m_teeth;
        private float m_time;
        private bool m_initialized;

        public void Initialize(Teeth teeth, float time)
        {
            m_teeth = teeth;
            m_time = time;
            m_circle.transform.localScale = Vector3.one * 2;
            m_initialized = true;
            transform.LookAt(Camera.main.transform);
        }

        private void Awake()
        {
            if(transform.childCount > 0)
            {
                m_circle = transform.GetChild(0).gameObject;
            }
            else
            {
                Debug.LogError("There is no child on Food object! (Add a child to represent the time left)");
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if(m_initialized)
            {
                if(m_time > 0)
                {
                    if(Circle.x > 1)
                    {
                        m_circle.transform.localScale -= Vector3.one * Time.deltaTime;
                    }
                    m_time -= Time.deltaTime;
                }
                else
                {
                    Remove();
                }
            }
        }

        public void Clear() => m_teeth.Clear();

        private void Remove() => m_teeth.Remove();

        private Vector3 Circle
        {
            get => m_circle.transform.localScale;
            set => m_circle.transform.localScale = value;
        }
    }
}