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
        /// The scale is modified when the value is changed.
        /// </summary>
        private Vector3 m_initialScale;

        /// <summary>
        /// Initial position for the Slider.
        /// The position is modified when the value is changed.
        /// </summary>
        private Vector3 m_initialPosition;

        /// <summary>
        /// Slider to visualize the Anger.
        /// </summary>
        private Slider m_slider;

        /// <summary>
        /// Previous Anger.
        /// Used to track the change in the Crocodiles Anger.
        /// Stored in a separate variable instead of using 
        /// the slider's value, since the slider has a limited range.
        /// </summary>
        private int m_anger;

        /*
         * Mono Behaviour Functions.
         */

        protected override void Awake()
        {
            base.Awake();
            //Cache the Slider component.
            m_slider = GetComponent<Slider>();
            //Store initial scale.
            m_initialScale = m_rect.localScale;
            //Store initial position.
            m_initialPosition = m_rect.position;
            UpdateFill();
        }

        /*
         * Functions.
         */

        public override void RequestUpdate()
        {
            //Update the Slider if the Anger has changed.
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
            //Update the stored anger.
            m_anger = CrocodileAnger;
            //Kill any active tween before resetting the position and scale.
            DOTween.Kill(transform);
            m_rect.localScale = m_initialScale;
            m_rect.position = m_initialPosition;
            //Check if the Crocodile's Anger is in the range of the Slider. 
            if(CrocodileAnger <= m_slider.maxValue)
            {
                //Update the slider to show the current anger.
                m_slider.value = CrocodileAnger;
                UpdateFill();
                //Shake the Slider to visualize change.
                transform.DOShakeScale(1);
                transform.DOShakePosition(0.5f, 2);
            }
            else
            {
                //Shake the Slider to visualize change.
                transform.DOShakeScale(1f, 0.5f);
                transform.DOShakePosition(2, 3);
            }
        }

        /// <summary>
        /// Update the visibility of the Slider's fill based on the Slider's current value.
        /// </summary>
        private void UpdateFill()
        {
            //Hide the Fill if the progress is zero.
            m_slider.fillRect.gameObject.SetActive((m_slider.value != 0));
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