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

        protected void Awake()
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
            DOTween.Sequence()
                .Append(transform.DOScale(1f, 0))
                .Append(transform.DOScale(1.5f, 0.4f).SetEase(Ease.OutBack))
                .Append(transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack))
                .SetUpdate(UpdateType.Manual)
                .Play();
        }

        private void OnMiss()
        {
            DOTween.Kill(transform);
            DOTween.Sequence()
                .Append(transform.DOScale(1f, 0))
                .Join(transform.DOShakeScale(1, 2))
                .SetUpdate(UpdateType.Manual)
                .Play();
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