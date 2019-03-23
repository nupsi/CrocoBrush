using UnityEngine;
using DG.Tweening;
using TMPro;

namespace CrocoBrush.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class QualityText : MonoBehaviour
    {
        private RectTransform m_transform;
        private Vector3 m_position;
        private TextMeshProUGUI m_text;

        private void Awake()
        {
            print("Awake");
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
            m_transform.position = m_position;
            m_text.SetText("");
        }

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