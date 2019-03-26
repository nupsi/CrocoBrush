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

        /// <summary>
        /// Parent object position for centering this object.
        /// </summary>
        private Vector3 m_position;

        /*
         * Mono Behaviour Functions.
         */

        private void Awake()
        {
            //Cache the rect transform for tweening.
            m_transform = GetComponent<RectTransform>();
            //Store the initial position, this should be the center of the parent object.
            m_position = m_transform.position;
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
            //Create new vector with randomized x and y position.
            var offset = new Vector3
            {
                x = m_position.x + Random.Range(-range, range),
                y = m_position.y + Random.Range(-range, range)
            };
            //Set the starting position.
            m_transform.position = offset;
        }

        private void OnDisable()
        {
            //Kill any possible tweens running on the transform.
            DOTween.Kill(m_transform);
            //Reset the position to initial position.
            m_transform.position = m_position;
            //Reset the text to none.
            //Maybe this is causing the rebuild issue below?
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
            //Update the text color.
            m_text.color = color;
            //Update the displayed text.
            m_text.SetText(text);
            //For some reason the text gets blurry after the SetText on build, so a manual rebuild is required.
            //This might cause some performance issues or defeat the purpose of object pooling the texts.
            m_text.Rebuild(CanvasUpdate.PreRender);
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
                .DOMoveY(m_transform.position.y + 100, duration)
                .SetEase(Ease.Linear)
                .OnComplete(() => parent.AddToPool(this));
        }
    }
}