using DG.Tweening;
using UnityEngine;

namespace CrocoBrush.UI.Menu
{
    public class ButtonAnimator : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_icon;

        [SerializeField]
        private bool m_rotate;

        private RectTransform m_rect;

        protected void Reset()
        {
            if(transform.childCount > 0)
            {
                m_icon = transform.GetChild(0).gameObject;
            }
        }

        public void OnStartHover()
        {
            Scale(1.1f);
            if(m_rotate)
            {
                Rotate(180);
            }
        }

        public void OnEndHover()
        {
            Rect.DOKill();
            Scale(1);
            Rect.DORotate(new Vector3(0, 0, 0), 0);
        }

        private void Scale(float scale)
        {
            Rect.DOScale(scale, 0.5f)
                .SetEase(Ease.OutQuart);
        }

        private void Rotate(float angle)
        {
            Rect.DORotate(new Vector3(0, 0, angle), 2)
                .SetEase(Ease.Linear)
                .OnComplete(() => Rotate(angle == 180 ? 360f : 180f));
        }

        private RectTransform Rect => m_rect ?? (m_rect = m_icon.GetComponent<RectTransform>());
    }
}