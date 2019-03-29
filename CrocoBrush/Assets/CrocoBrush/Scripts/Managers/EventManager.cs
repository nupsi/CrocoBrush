using System.Collections.Generic;
using UnityEngine.Events;

namespace CrocoBrush
{
    public class EventManager
    {
        private Dictionary<string, UnityEvent> m_events;

        private static EventManager m_instance;

        public EventManager()
        {
            m_instance = this;
            m_events = new Dictionary<string, UnityEvent>();
        }

        public void StartListening(string name, UnityAction listener)
        {
            if(m_events.TryGetValue(name, out var current))
            {
                current.AddListener(listener);
            }
            else
            {
                current = new UnityEvent();
                current.AddListener(listener);
                m_events.Add(name, current);
            }
        }

        public void StopListening(string name, UnityAction listener)
        {
            if(m_instance == null) return;
            if(m_events.TryGetValue(name, out var current))
            {
                current.RemoveListener(listener);
            }
        }

        public void TriggerEvent(string name)
        {
            if(m_events.TryGetValue(name, out var current))
            {
                current.Invoke();
            }
        }

        public void Clear() => m_events.Clear();

        public static EventManager Instance => m_instance ?? new EventManager();
    }
}