using DG.Tweening;
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
            //Display Spacebar hitting.
            m_graphics.SetActive(true);
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
                        .OnComplete(ClearSpace)
                    )
                );
        }

        public void ClearSpace()
        {
            DOTween.Kill(m_graphics.transform);
            if(m_graphics.activeInHierarchy)
            {
                m_graphics.SetActive(false);
                Crocodile.Instance.AddScore(2);
            }
            else
            {
                Crocodile.Instance.Annoy();
            }
        }
    }
}