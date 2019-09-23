using UnityEngine;

namespace CrocoBrush.Events
{
    public class LoadEvent : MonoBehaviour
    {
        [SerializeField]
        private CustomEvent m_awakeEvents;

        [SerializeField]
        private CustomEvent m_startEvents;

        protected void Awake()
        {
            m_awakeEvents.Invoke();
        }

        protected void Start()
        {
            m_startEvents.Invoke();
        }
    }
}