using UnityEngine;

namespace CrocoBrush.Events
{
    public class LoadEvent : MonoBehaviour
    {
        [SerializeField]
        private CustomEvent m_awakeEvents;

        [SerializeField]
        private CustomEvent m_startEvents;

        private void Awake() 
        {
            m_awakeEvents.Invoke();
        }

        private void Start()
        {
            m_startEvents.Invoke();
        }
    }
}