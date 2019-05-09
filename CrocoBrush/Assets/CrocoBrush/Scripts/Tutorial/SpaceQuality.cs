using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CrocoBrush.Tutorial
{
    public class SpaceQuality : MonoBehaviour
    {
        [SerializeField]
        private Image m_image;

        [SerializeField]
        private TextMeshProUGUI m_text;

        private float m_duration = 2f;

        private Sequence m_sequence;

        private void OnEnable()
        {
            Loop();
            StartCoroutine(UpdateText());
        }

        private void OnDisable()
        {
            m_sequence.Kill();
        }

        private void Loop()
        {
            m_sequence = DOTween.Sequence()
                .OnStart(() => StartCoroutine(Degrade()))
                .Append(m_image.transform.DOScale(0.1f, 0f))
                .Append(m_image.transform.DOScale(1f, m_duration).SetEase(Ease.Linear))
                .Append(m_image.transform.DOScale(1f, 0.3f).SetEase(Ease.Linear))
                .Play();
        }

        private IEnumerator UpdateText()
        {
            var wait = new WaitForSeconds(0.1f);
            while(this.isActiveAndEnabled)
            {
                m_text.color = Quality == Quality.Bad
                    ? Color.red
                    : Quality == Quality.Good
                        ? Color.blue
                        : Color.green;
                m_text.SetText(Quality.ToString());
                yield return wait;
            }
        }

        private IEnumerator Degrade()
        {
            yield return new WaitForSeconds(m_duration * 0.4f);
            Quality = Quality.Good;
            yield return new WaitForSeconds(m_duration * 0.5f);
            Quality = Quality.Perfect;
            yield return new WaitForSeconds(0.29f);
            Quality = Quality.Bad;
            Loop();
        }

        private Quality Quality = Quality.Bad;
    }
}