using DG.Tweening;
using UnityEngine;

namespace CrocoBrush.Animation
{
    [RequireComponent(typeof(Bird))]
    public class BirdAnimator : StartEndAnimator
    {
        [SerializeField]
        private GameObject m_start;

        [SerializeField]
        private GameObject m_mouth;

        private Bird m_bird;

        protected override void Awake()
        {
            base.Awake();
            m_bird = GetComponent<Bird>();
            m_bird.enabled = false;
            m_bird.transform.position = m_start.transform.position;
        }

        private void OnDrawGizmosSelected()
        {
            if(m_start != null && m_mouth != null)
            {
                Gizmos.DrawLine(m_start.transform.position, m_mouth.transform.position);
            }
        }

        protected override void LevelStart()
        {
            m_bird.enabled = true;
            DOTween.Kill(m_bird.transform);
            m_bird.transform.DOMove(m_mouth.transform.position, 0.6f);
        }

        protected override void LevelEnd()
        {
            m_bird.enabled = false;
            DOTween.Kill(m_bird.transform);
            m_bird.transform.DOMove(m_start.transform.position, 0.6f);
        }
    }
}