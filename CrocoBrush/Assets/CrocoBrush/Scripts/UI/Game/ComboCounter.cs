using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CrocoBrush.UI.Game
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ComboCounter : RegisteredBehaviour
    {
        private TextMeshProUGUI m_text;
        private int m_combo;

        private void Awake()
        {
            m_text = GetComponent<TextMeshProUGUI>();
        }

        private void OnHit()
        {
            DOTween.Kill(transform);
            transform.DOScale(1f, 0);
            m_combo++;
            transform.DOScale(1.5f, 0.4f).SetEase(Ease.OutBack).OnComplete(
                () => transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
            UpdateText();

            if(m_combo % 10 == 0)
            {
                Crocodile.Instance.CalmDown();
            }
        }

        private void OnMiss()
        {
            if(m_combo > 0)
            {
                DOTween.Kill(transform);
                transform.DOScale(1f, 0);
                transform.DOShakeScale(1, 2);
                m_combo = 0;
                UpdateText();
            }
        }

        private void ResetComponent()
        {
            m_combo = 0;
            UpdateText();
        }

        private void UpdateText()
        {
            m_text.SetText($"Combo x {m_combo}");
        }

        protected override Dictionary<string, Action> Actions =>
            m_actions ??
            (m_actions = new Dictionary<string, Action>
            {
                { "Hit", OnHit },
                { "Miss", OnMiss },
                { "ResetGame", ResetComponent }
            });
    }
}