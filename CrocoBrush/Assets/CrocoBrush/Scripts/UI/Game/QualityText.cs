using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CrocoBrush.UI.Game
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

        /*
         * Mono Behaviour Functions.
         */

        private void Awake()
        {
            //Cache the rect transform for tweening.
            m_transform = GetComponent<RectTransform>();
            //Cache the text field component and set its properties.
            m_text = GetComponent<TextMeshProUGUI>();
            m_text.alignment = TextAlignmentOptions.Center;
            m_text.enableWordWrapping = false;
            m_text.raycastTarget = false;
            m_text.richText = false;
        }

        private void OnEnable()
        {
            //Offset range for the text's position.
            var range = 50;
            //Set new position with randomized x and y position.
            m_transform.localPosition = new Vector3
            {
                x = Random.Range(-range, range),
                y = Random.Range(-range, range)
            };
        }

        private void OnDisable()
        {
            //Kill any possible tweens running on the transform.
            DOTween.Kill(m_transform);
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
            //Update the text color.
            m_text.color = color;
            //Update the displayed text.
            if(m_text.text != text)
            {
                m_text.SetText(text);
                m_text.Rebuild(CanvasUpdate.PreRender);
            }
            //For some reason the text gets blurry after the SetText on build, so a manual rebuild is required.
            //This might cause some performance issues or defeat the purpose of object pooling the texts.
            //
            //Tween the Size.
            m_transform
                .DOScale(0.1f, 0.001f)
                .SetEase(Ease.Linear)
                .OnComplete(
                    () => m_transform
                    .DOScale(1, duration)
                    .SetEase(Ease.OutBack));
            //Tween the position upwards.
            m_transform
                .DOLocalMoveY(m_transform.localPosition.y + 100, duration)
                .SetEase(Ease.Linear)
                .OnComplete(() => parent.AddToPool(this));
        }
    }
}