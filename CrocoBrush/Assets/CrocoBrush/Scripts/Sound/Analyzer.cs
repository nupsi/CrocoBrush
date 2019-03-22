using UnityEngine;

namespace Test.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class Analyzer : MonoBehaviour
    {
        private AudioSource m_source;
        private AudioSamples m_samples;

        private float[] m_current;
        private bool m_reset;

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