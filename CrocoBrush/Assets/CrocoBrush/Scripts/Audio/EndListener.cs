using UnityEngine;

namespace CrocoBrush.Audio
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Activator))]
    public class EndListener : MonoBehaviour
    {
        private AudioSource m_source;
        private Activator m_activator;

        protected void Awake()
        {
            m_source = GetComponent<AudioSource>();
            m_activator = GetComponent<Activator>();
        }

        protected void Update()
        {
            if(m_source.time >= m_source.clip.length)
            {
                EventManager.Instance.TriggerEvent("LevelEnd", "LevelWin");
                Crocodile.Instance.SaveStats();
                m_activator.Activate();
            }
        }
    }
}