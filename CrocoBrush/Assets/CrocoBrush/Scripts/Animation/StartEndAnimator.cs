using UnityEngine;

namespace CrocoBrush.Animation
{
    [RequireComponent(typeof(Animator))]
    public abstract class StartEndAnimator : MonoBehaviour
    {
        protected Animator m_animator;

        protected virtual void Awake()
        {
            m_animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            EventManager.Instance.StartListening("LevelStart", LevelStart);
            EventManager.Instance.StartListening("LevelEnd", LevelEnd);
        }

        private void OnDisable()
        {
            EventManager.Instance.StopListening("LevelStart", LevelStart);
            EventManager.Instance.StopListening("LevelEnd", LevelEnd);
        }

        protected abstract void LevelStart();

        protected abstract void LevelEnd();
    }
}