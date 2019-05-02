using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(AudioSource))]
    public class AmbiencePlayer : RegisteredBehaviour
    {
        private AudioSource m_source;

        private void Awake()
        {
            m_source = GetComponent<AudioSource>();
        }

        protected void LevelStart() => m_source.Pause();

        protected void LevelEnd() => m_source.Play();

        protected override Dictionary<string, Action> Actions =>
            m_actions ??
            (m_actions = new Dictionary<string, Action>
            {
                        { "LevelStart", LevelStart },
                        { "LevelEnd", LevelEnd }
            });
    }
}