using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CrocoBrush.UI.Game
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ComboCounter : GUIGame
    {
        private TextMeshProUGUI m_text;
        private int m_streak;

        private void Awake()
        {
            m_text = GetComponent<TextMeshProUGUI>();
        }

        protected override void UpdateComponent()
        {
            if(m_streak != Streak)
            {
                if(m_streak < Streak)
                {
                    OnHit();
                }
                else
                {
                    OnMiss();
                }
                m_streak = Streak;
            }

            UpdateText();
        }

        private void OnHit()
        {
            DOTween.Kill(transform);
            transform.DOScale(1f, 0);
            transform.DOScale(1.5f, 0.4f).SetEase(Ease.OutBack).OnComplete(
                () => transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
        }

        private void OnMiss()
        {
            DOTween.Kill(transform);
            transform.DOScale(1f, 0);
            transform.DOShakeScale(1, 2);
        }

        protected override void ResetComponent()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            m_text.SetText($"Combo x {Streak}");
        }

        private int Streak => Crocodile.Instance.Streak;
    }
}