using System.Collections;
using UnityEngine;

namespace CrocoBrush.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class Analyzer : MonoBehaviour
    {
        protected AudioSource m_source;
        protected AudioSamples m_samples;

        protected float[] m_current;
        protected bool m_reset;
        protected int m_channel = 6;

        private void Awake()
        {
            m_source = GetComponent<AudioSource>();
            m_samples = new AudioSamples(m_source);
            m_current = new float[8];
            StartCoroutine(UpdateSongTime());
        }

        private IEnumerator UpdateSongTime()
        {
            var wait = new WaitForSeconds(0.00001f);
            while(true)
            {
                if(m_source.isPlaying)
                {
                    m_samples.Update();
                    m_current = m_samples.FrequencyBands;
                    if(m_current[m_channel] >= 0.05f && !m_reset)
                    {
                        m_reset = true;
                        RequestInput();
                    }
                    else if(m_current[m_channel] <= 0.05f && m_reset)
                    {
                        m_reset = false;
                    }
                }
                yield return wait;
            }
        }

        protected virtual void RequestInput()
        {
        }
    }
}