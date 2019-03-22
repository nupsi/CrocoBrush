using UnityEngine;

namespace Test.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class Analyzer : MonoBehaviour
    {
        protected AudioSource m_source;
        protected AudioSamples m_samples;

        protected float[] m_current;
        protected bool m_reset;

        private void Awake()
        {
            m_source = GetComponent<AudioSource>();
            m_samples = new AudioSamples(m_source);
            m_current = new float[8];
        }

        private void Update()
        {
            if(m_source.isPlaying)
            {
                m_samples.Update();
                m_current = m_samples.FrequencyBands;
                if(m_current[6] >= 0.05f && !m_reset)
                {
                    m_reset = true;
                    RequestInput();
                }
                else if(m_current[6] <= 0.05f && m_reset)
                {
                    m_reset = false;
                }
            }
        }

        protected virtual void RequestInput()
        {
        }
    }
}