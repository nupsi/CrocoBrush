using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CrocoBrush.Tutorial
{
    /// <summary>
    /// Displays how food acts over time.
    /// </summary>
    public class FoodQuality : MonoBehaviour
    {
        [SerializeField]
        private RawImage m_circle;

        [SerializeField]
        private RawImage m_fail;

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
            m_fail.gameObject.SetActive(false);
            m_circle.gameObject.SetActive(true);
            m_sequence = DOTween.Sequence()
                .OnStart(() =>
                {
                    StartCoroutine(Degrade());
                    m_circle.transform.localScale = Vector3.one * 2;
                })
                .Append(m_circle.transform.DOScale(Vector3.one, m_duration).SetEase(Ease.Linear))
                .Append(m_circle.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.Linear))
                .OnComplete(() =>
                {
                    Quality = Quality.Bad;
                    DOTween.Sequence()
                        .OnStart(() =>
                        {
                            m_circle.gameObject.SetActive(false);
                            m_fail.gameObject.SetActive(true);
                        })
                        .Append(transform.DOScale(0.5f, 0.3f).SetEase(Ease.InBack))
                        .Append(transform.DOScale(1, 0))
                        .OnComplete(Loop)
                        .Play();
                })
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
            yield return new WaitForSeconds(m_duration * 0.5f);
            Quality = Quality.Good;
            yield return new WaitForSeconds(m_duration * 0.5f);
            Quality = Quality.Perfect;
        }

        private Quality Quality = Quality.Bad;
    }
}