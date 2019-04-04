using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace CrocoBrush
{
    public class Spacebar : MonoBehaviour, ICreator
    {
        public static Spacebar Instance;

        private GameObject m_graphics;

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
        }

        private void Update()
        {
            if(Input.GetButtonDown("Jump"))
            {
                ClearSpace();
            }
        }

        public void Create(Direction direction)
        {
            //"Reset" the spacebar.
            m_graphics.SetActive(true);
            Quality = Quality.Bad;

            //Start the spacebar logic.
            StartCoroutine(Degrade(Mouth.Instance.Delay));
            m_graphics.transform
                .DOScale(0.1f, 0.01f)
                .OnComplete(
                    () => m_graphics.transform
                    .DOScale(1f, Mouth.Instance.Delay)
                    .SetEase(Ease.Linear)
                    .OnComplete(
                        () => m_graphics.transform
                        .DOScale(1f, 0.3f)
                        .SetEase(Ease.Linear)
                        .OnComplete(() =>
                            {
                                Quality = Quality.Bad;
                                ClearSpace();
                            })
                    )
                );
        }

        public void ClearSpace()
        {
            DOTween.Kill(m_graphics.transform);
            m_graphics.SetActive(false);
            switch(Quality)
            {
                case Quality.Bad:
                    Crocodile.Instance.Annoy();
                    EventManager.Instance.TriggerEvent("Miss");
                    break;

                case Quality.Good:
                    Crocodile.Instance.AddScore(1);
                    EventManager.Instance.TriggerEvent("SpaceHit");
                    break;

                case Quality.Perfect:
                    Crocodile.Instance.AddScore(2);
                    EventManager.Instance.TriggerEvent("SpaceHit");
                    break;
            }
        }

        private IEnumerator Degrade(float duration)
        {
            yield return new WaitForSeconds(duration * 0.4f);
            Quality = Quality.Good;
            yield return new WaitForSeconds(duration * 0.5f);
            Quality = Quality.Perfect;
            yield return new WaitForSeconds(0.29f);
            Quality = Quality.Bad;
        }

        public Quality Quality { get; private set; }
    }
}