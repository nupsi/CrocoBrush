using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CrocoBrush.Tutorial
{
    /// <summary>
    /// Countdown text display.
    /// Countdown is started on OnEnable and automatically disabled when the countdown ends.
    /// </summary>
    public class Countdown : MonoBehaviour
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Countdown texts displayed in linear order, starting from zero.
        /// </summary>
        private readonly string[] m_countdown = new string[]
        {
            "Get Ready!",
            "3",
            "2",
            "1",
            "GO"
        };

        /// <summary>
        /// Text Field to display the countdown.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI m_text;

        /// <summary>
        /// Tween sequence for the countdown.
        /// </summary>
        private Sequence m_sequence;

        /// <summary>
        /// Current countdown position.
        /// </summary>
        private int m_current;

        /*
         * Mono Behaviour Functions.
         */

        protected void OnEnable()
        {
            m_current = 0;
            Display();
        }

        protected void OnDisable()
        {
            m_sequence.Kill();
        }

        /*
         * Functions.
         */

        /// <summary>
        /// Display the next step of the countdown.
        /// If there are no more steps the textfield is disabled.
        /// </summary>
        private void Display()
        {
            if(m_current < m_countdown.Length)
            {
                m_text.gameObject.SetActive(true);
                m_text.SetText(m_countdown[m_current]);
                m_sequence = DOTween.Sequence()
                    .Append(transform.DOScale(1, 0))
                    .Append(transform.DOScale(1, 0.25f))
                    .Append(transform.DOScale(0.5f, 0.75f))
                    .OnComplete(() =>
                    {
                        m_current++;
                        Display();
                    })
                    .Play();
            }
            else
            {
                m_text.gameObject.SetActive(false);
            }
        }
    }
}