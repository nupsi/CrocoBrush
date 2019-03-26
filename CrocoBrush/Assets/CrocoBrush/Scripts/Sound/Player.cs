using UnityEngine;

namespace CrocoBrush.Sound
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private AudioSource m_source;

        private bool m_reset;

        public void TogglePause()
        {
            if(m_source.isPlaying)
            {
                Debug.Log("Pause");
                m_source.Pause();
            }
            else
            {
                if(m_reset)
                {
                    Debug.Log("Play");
                    m_source.Play();
                    m_reset = false;
                }
                else
                {
                    Debug.Log("Return");
                    m_source.UnPause();
                }
            }
        }

        public void Stop()
        {
            Debug.Log("Stop");
            m_source.Stop();
            m_reset = true;
        }
    }
}