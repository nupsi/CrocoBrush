using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush.Animation
{
    [RequireComponent(typeof(Animator))]
    public abstract class StartEndAnimator : RegisteredBehaviour
    {
        protected Animator m_animator;

        protected virtual void Awake()
        {
            m_animator = GetComponent<Animator>();
        }

        protected abstract void LevelStart();

        protected abstract void LevelEnd();

        protected override Dictionary<string, Action> Actions =>
            m_actions ??
            (m_actions = new Dictionary<string, Action>
            {
                { "LevelStart", LevelStart },
                { "LevelEnd", LevelEnd }
            });
    }
}