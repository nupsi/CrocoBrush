using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : RegisteredBehaviour
    {
        [SerializeField]
        private string m_eventName = "Hit";

        protected AudioSource m_source;

        private void Awake()
        {
            m_source = GetComponent<AudioSource>();
        }

        private void Reset()
        {
            m_source = GetComponent<AudioSource>();
            m_source.playOnAwake = false;
        }

        protected override void UpdateComponent()
        {
            m_source.Play();
        }

        protected override string EventName => m_eventName;
    }
}