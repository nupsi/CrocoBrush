using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace CrocoBrush.UI.Menu
{
    /// <summary>
    /// Allows to control audio mixers groups volumes during runtime.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class VolumeController : GUIMenu
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Sliders Max Value (Set on Reset()).
        /// </summary>
        private readonly float MaxValue = 1;

        /// <summary>
        /// Sliders Min Value (Set on Reset()).
        /// </summary>
        private readonly float MinValue = 0.0001f;

        /// <summary>
        /// Sliders Default Value (Set on Reset()).
        /// </summary>
        private readonly float DefaultValue = 1;

        /// <summary>
        /// Audio mixer that contains the modified group.
        /// </summary>
        [SerializeField]
        private AudioMixer m_mixer;

        /// <summary>
        /// Group to modify.
        /// You need to expose a group and then give it the name.
        /// Enter the given name here inside Unity.
        /// </summary>
        [SerializeField]
        private string m_group;

        /// <summary>
        /// Slider to control the volume with.
        /// </summary>
        private Slider m_slider;

        /*
         * Mono Behaviour Functions.
         */

        protected void Awake()
        {
            m_slider = GetComponent<Slider>();
            m_slider.value = PlayerPrefs.HasKey(m_group)
                ? PlayerPrefs.GetFloat(m_group)
                : DefaultValue;
        }

        private void Reset()
        {
            m_slider = GetComponent<Slider>();
            m_slider.minValue = MinValue;
            m_slider.maxValue = MaxValue;
            m_slider.value = DefaultValue;
        }

        /*
         * Functions.
         */

        /// <summary>
        /// Updates the group's volume to the given value.
        /// </summary>
        public void SetVolume(float volume)
        {
            if(volume >= MinValue && volume <= MaxValue)
            {
                m_mixer.SetFloat(m_group, Mathf.Log10(volume) * 20);
                PlayerPrefs.SetFloat(m_group, m_slider.value);
            }
            else
            {
                Debug.LogError($"The given volume ({volume}) is outside the given range ({MinValue} - {MaxValue})");
            }
        }

        /// <summary>
        /// Used to reset the value to the default value.
        /// </summary>
        protected override void UpdateComponent()
        {
            if(PlayerPrefs.HasKey(m_group))
            {
                SetVolume(DefaultValue);
            }
        }
    }
}