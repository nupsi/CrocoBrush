﻿using UnityEngine;
using UnityEngine.UI;

namespace CrocoBrush.UI.Game
{
    /// <summary>
    /// Progress bar to display Crocodiles Anger.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    [RequireComponent(typeof(Activator))]
    public class AngerBar : GUIGame
    {
        /*
         * Variables.
         */

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

        private Activator m_activator;

        /*
         * Mono Behaviour Functions.
         */

        protected void Awake()
        {
            m_slider = GetComponent<Slider>();
            m_activator = GetComponent<Activator>();
            UpdateFill();
        }

        /*
         * Functions.
         */

        protected override void UpdateComponent()
        {
            //Update the Slider if the Anger has changed.
            if(m_anger != CrocodileAnger)
            {
                UpdateSlider();
            }
        }

        protected override void ResetComponent()
        {
            UpdateSlider();
        }

        /// <summary>
        /// Update Slider to display latest Anger.
        /// </summary>
        private void UpdateSlider()
        {
            //Update the stored anger.
            m_anger = CrocodileAnger;
            //Check if the Crocodile's Anger is in the range of the Slider.
            if(CrocodileAnger <= m_slider.maxValue)
            {
                //Update the slider to show the current anger.
                m_slider.value = CrocodileAnger;
                UpdateFill();
            }
            else
            {
                LevelController.Instance.Stop();
                EventManager.Instance.TriggerEvent("LevelEnd", "LevelLose");
                m_activator.Activate();
            }
        }

        /// <summary>
        /// Update the visibility of the Slider's fill based on the Slider's current value.
        /// </summary>
        private void UpdateFill()
        {
            //Hide the Fill if the progress is zero.
            m_slider.fillRect.gameObject.SetActive(m_slider.value != 0);
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