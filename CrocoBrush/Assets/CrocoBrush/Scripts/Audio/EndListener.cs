using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(AudioSource))]
    public class EndListener : MonoBehaviour
    {
        private AudioSource m_source;

        private void Awake()
        {
            m_source = GetComponent<AudioSource>();
        }

        private void FixedUpdate()
        {
            if(m_source.time >= m_source.clip.length)
            {
                EventManager.Instance.TriggerEvent("LevelEnd");
            }
        }
    }
}