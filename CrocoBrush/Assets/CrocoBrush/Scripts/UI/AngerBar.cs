using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CrocoBrush.UI
{
    /// <summary>
    /// Progress bar to display Crocodiles Anger.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class AngerBar : GUIBehaviour
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Initial Scale for the Slider.
        /// </summary>
        private Vector3 m_initialScale;

        /// <summary>
        /// Initial position for the Slider.
        /// </summary>
        private Vector3 m_initialPosition;

        /// <summary>
        /// Slider to visualize Anger.
        /// </summary>
        private Slider m_progress;

        /// <summary>
        /// Previous Anger.
        /// </summary>
        private int m_anger;

        /*
         * Mono Behaviour Functions.
         */

        protected override void Awake()
        {
            base.Awake();
            m_progress = GetComponent<Slider>();
            m_initialScale = m_rect.localScale;
            m_initialPosition = m_rect.position;
            UpdateFill();
        }

        /*
         * Functions.
         */

        public override void RequestUpdate()
        {
            if(m_anger != CrocodileAnger)
            {
                UpdateSlider();
            }
        }

        /// <summary>
        /// Update Slider to display latest Anger.
        /// </summary>
        private void UpdateSlider()
        {
            m_anger = CrocodileAnger;
            DOTween.Kill(transform);
            m_rect.localScale = m_initialScale;
            m_rect.position = m_initialPosition;
            if(Crocodile.Instance.Anger <= m_progress.maxValue)
            {
                m_progress.value = CrocodileAnger;
                UpdateFill();
                transform.DOShakeScale(1);
                transform.DOShakePosition(0.5f, 2);
            }
            else
            {
                transform.DOShakeScale(1f, 0.5f);
                transform.DOShakePosition(2, 3);
            }
        }

        private void UpdateFill()
        {
            m_progress.fillRect.gameObject.SetActive((m_progress.value != 0));
        }

        /*
         * Accessors.
         */

        /// <summary>
        /// Crocodile's current Anger.
        /// </summary>
        /// <value>Crocodile's current Anger.</value>
        private int CrocodileAnger => Crocodile.Instance.Anger;
    }
}