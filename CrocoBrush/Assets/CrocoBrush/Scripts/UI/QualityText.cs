using UnityEngine;
using DG.Tweening;
using TMPro;

namespace CrocoBrush.UI
{
    /// <summary>
    /// Automatically generated Text Mesh Pro Text Field to display text with tween.
    /// Use DisplayText(DisplayQuality, string, float, Color) to display text.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class QualityText : MonoBehaviour
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Cached Text mesh pro text field component.
        /// </summary>
        private TextMeshProUGUI m_text;

        /// <summary>
        /// Components rect transform for tweening.
        /// </summary>
        private RectTransform m_transform;

        /// <summary>
        /// Parent object position for centering this object.
        /// </summary>
        private Vector3 m_position;

        /*
         * Mono Behaviour Functions.
         */

        private void Awake()
        {
            m_transform = GetComponent<RectTransform>();
            m_position = m_transform.position;
            m_text = GetComponent<TextMeshProUGUI>();
            m_text.alignment = TextAlignmentOptions.Center;
        }

        private void OnEnable()
        {
            var range = 50;
            var offset = new Vector3
            {
                x = m_position.x + Random.Range(-range, range),
                y = m_position.y + Random.Range(-range, range)
            };
            m_transform.localScale = new Vector2(0.1f, 0.1f);
            m_transform.position = offset;
        }

        private void OnDisable()
        {
            DOTween.Kill(m_transform);
            m_transform.position = m_position;
            m_text.SetText("");
        }

        /*
         * Functions.
         */

        /// <summary>
        /// Display given text with a tween lasting for a given duration with a given color.
        /// Display Quality component is required for returning the quality text back to object pool.
        /// </summary>
        /// <param name="parent">Parent object for pooling.</param>
        /// <param name="text">Text to display.</param>
        /// <param name="duration">Duration for the tween.</param>
        /// <param name="color">Color for the text.</param>
        public void DisplayText(DisplayQuality parent, string text, float duration, Color color)
        {
            m_position = parent.transform.position;
            m_text.color = color;
            m_text.SetText(text);
            m_transform
                .DOScale(1, duration * 0.5f)
                .SetEase(Ease.OutBack);
            m_transform
                .DOMoveY(m_transform.position.y + 100, duration)
                .SetEase(Ease.Linear)
                .OnComplete(() => parent.AddToPool(this));
        }
    }
}