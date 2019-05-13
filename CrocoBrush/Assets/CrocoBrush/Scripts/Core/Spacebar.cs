using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace CrocoBrush
{
    public class Spacebar : MonoBehaviour, ICreator
    {
        public static Spacebar Instance;

        private GameObject m_graphics;

        private Sequence m_tween;

        private IEnumerator m_coroutine;

        private void Awake()
        {
            if(Instance != null)
            {
                Debug.LogError("Multiple spacebar Instances!");
                return;
            }
            Instance = this;
            m_graphics = transform.GetChild(0).gameObject;
            m_graphics.SetActive(false);
            Show(false);
        }

        public void Create(Direction direction)
        {
            Show(true);
            Quality = Quality.Bad;
            StartCoroutine(m_coroutine = Degrade(Mouth.Instance.Delay));
            m_tween = DOTween.Sequence()
                .Append(Graphics.DOScale(0.1f, 0f))
                .Append(Graphics.DOScale(1f, Mouth.Instance.Delay).SetEase(Ease.Linear))
                .Append(Graphics.DOScale(1f, 0.3f).SetEase(Ease.Linear))
                .SetUpdate(UpdateType.Manual)
                .Play();
        }

        public void ClearSpace()
        {
            if(Visible)
            {
                m_tween.Kill();
                StopCoroutine(m_coroutine);
                Show(false);
                Crocodile.Instance.AddScore(Quality);
            }
        }

        private IEnumerator Degrade(float duration)
        {
            yield return new WaitForSeconds(duration * 0.5f);
            Quality = Quality.Good;
            yield return new WaitForSeconds(duration * 0.5f);
            Quality = Quality.Perfect;
            yield return new WaitForSeconds(0.3f);
            Quality = Quality.Bad;
            ClearSpace();
        }

        private void Show(bool show)
        {
            Graphics.DOScale(show ? 1f : 0.1f, 0);
            m_graphics.SetActive(show);
            Visible = show;
        }

        public Quality Quality { get; private set; }

        public bool Visible { get; private set; }

        private Transform Graphics => m_graphics.transform;
    }
}