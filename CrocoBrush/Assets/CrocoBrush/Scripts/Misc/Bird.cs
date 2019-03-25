using UnityEngine;
using DG.Tweening;

namespace CrocoBrush
{
    public class Bird : MonoBehaviour
    {
        public GameObject m_bottom;
        public GameObject m_top;
        public GameObject m_left;
        public GameObject m_right;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                DOTween.Kill(transform.position);
                transform.DOMove(m_left.transform.position, 0.5f);
            }

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                DOTween.Kill(transform.position);
                transform.DOMove(m_right.transform.position, 0.5f);
            }

            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                DOTween.Kill(transform.position);
                transform.DOMove(m_bottom.transform.position, 0.5f);
            }

            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                DOTween.Kill(transform.position);
                transform.DOMove(m_top.transform.position, 0.5f);
            }
        }
    }
}