using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace CrocoBrush.UI.Menu
{
    /// <summary>
    /// Allows to control audio mixers groups volumes during runtime.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class VolumeController : MonoBehaviour
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Sliders Max Value (Set on Reset()).
        /// </summary>
        private readonly float MaxValue = 0;

        /// <summary>
        /// Sliders Min Value (Set on Reset()).
        /// </summary>
        private readonly float MinValue = -80;

        /// <summary>
        /// Sliders Default Value (Set on Reset()).
        /// </summary>
        private readonly float DefaultValue = 0;

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

        private void Awake()
        {
            m_slider = GetComponent<Slider>();
            var volume = m_slider.value;
            m_mixer.GetFloat(m_group, out volume);
            m_slider.value = volume;
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
        /// Updates the group's volume to the current slider value.
        /// </summary>
        public void UpdateVolume()
        {
            m_mixer.SetFloat(m_group, m_slider.value);
        }
    }
}