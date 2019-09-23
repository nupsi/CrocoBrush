using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CrocoBrush.UI.Menu
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class FadeText : MonoBehaviour
    {
        [SerializeField]
        private float m_duration = 1.5f;

        private TextMeshProUGUI m_text;

        protected void Awake()
        {
            m_text = GetComponent<TextMeshProUGUI>();
        }

        protected void OnEnable() => m_text.DOFade(1, 0).OnComplete(() => Fade(0));

        protected void OnDisable() => m_text.DOKill();

        private void Fade(int alpha) => m_text.DOFade(alpha, m_duration).OnComplete(() => Fade(alpha == 0 ? 1 : 0));
    }
}