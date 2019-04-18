using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush.Audio
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

        protected void UpdateComponent()
        {
            m_source.Play();
        }

        protected override Dictionary<string, Action> Actions =>
            m_actions ??
            (m_actions = new Dictionary<string, Action>
            {
                { m_eventName, UpdateComponent }
            });
    }
}