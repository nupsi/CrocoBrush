using UnityEngine;

namespace CrocoBrush.Audio
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Activator))]
    public class EndListener : MonoBehaviour
    {
        private AudioSource m_source;
        private Activator m_activator;

        private void Awake()
        {
            m_source = GetComponent<AudioSource>();
            m_activator = GetComponent<Activator>();
        }

        private void Update()
        {
            if(m_source.time >= m_source.clip.length)
            {
                EventManager.Instance.TriggerEvent("LevelEnd");
                EventManager.Instance.TriggerEvent("LevelWin");
                m_activator.Activate();
            }
        }
    }
}