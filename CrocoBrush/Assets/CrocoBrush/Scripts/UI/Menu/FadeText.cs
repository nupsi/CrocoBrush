using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CrocoBrush.UI.Menu
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class FadeText : MonoBehaviour
    {
        private TextMeshProUGUI m_text;
        private float m_time = 1.5f;

        private void Awake()
        {
            m_text = GetComponent<TextMeshProUGUI>();
            Fade(0);
        }

        private void OnEnable()
        {
            m_text.DOFade(1, 0)
                .OnComplete(() => Fade(0));
        }

        private void OnDisable()
        {
            DOTween.Kill(m_text);
        }

        private void Fade(int target) => m_text.DOFade(target, m_time).OnComplete(() => Fade(target == 0 ? 1 : 0));
    }
}