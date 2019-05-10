using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CrocoBrush.Tutorial
{
    public class Countdown : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_text;

        private Sequence m_sequence;
        private int m_current;

        private string[] m_countdown = new string[]
        {
            "Get Ready!",
            "3",
            "2",
            "1",
            "GO"
        };

        private void OnEnable()
        {
            m_current = 0;
            Display();
        }

        private void OnDisable()
        {
            m_sequence.Kill();
        }

        private void Display()
        {
            if(m_current < m_countdown.Length)
            {
                m_text.gameObject.SetActive(true);
                m_text.SetText(m_countdown[m_current]);
                m_sequence = DOTween.Sequence()
                    .Append(transform.DOScale(1, 0))
                    .Append(transform.DOScale(1, 0.25f))
                    .Append(transform.DOScale(0.5f, 0.75f))
                    .OnComplete(() =>
                    {
                        m_current++;
                        Display();
                    })
                    .Play();
            }
            else
            {
                m_text.gameObject.SetActive(false);
            }
        }
    }
}