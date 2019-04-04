using System.Collections.Generic;

namespace CrocoBrush
{
    public class EventManager
    {
        private Dictionary<string, Action> m_events;

        private static EventManager m_instance;

        public EventManager()
        {
            m_instance = this;
            m_events = new Dictionary<string, Action>();
        }

        public void StartListening(string name, Action listener)
        {
            if(m_events.TryGetValue(name, out var current))
            {
                current += listener;
                m_events[name] += current;
            }
            else
            {
                current += listener;
                m_events.Add(name, current);
            }
        }

        public void StopListening(string name, Action listener)
        {
            if(m_instance == null) return;
            if(m_events.TryGetValue(name, out var current))
            {
                current -= listener;
                m_events[name] = current;
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