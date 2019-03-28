using DG.Tweening;
using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(Animator))]
    public class Bird : MonoBehaviour
    {
        private readonly string Eat = "Eat";
        private readonly string Block = "Block";

        public GameObject m_bottom;
        public GameObject m_top;
        public GameObject m_left;
        public GameObject m_right;

        private Animator m_aimator;

        private void Awake()
        {
            m_aimator = GetComponent<Animator>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                DOTween.Kill(transform.position);
                transform.DOMove(m_left.transform.position, 0.5f)
                         .OnComplete(() => m_aimator.SetTrigger(Eat));
            }

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                DOTween.Kill(transform.position);
                transform.DOMove(m_right.transform.position, 0.5f)
                         .OnComplete(() => m_aimator.SetTrigger(Eat));
            }

            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                DOTween.Kill(transform.position);
                transform.DOMove(m_bottom.transform.position, 0.5f)
                         .OnComplete(() => m_aimator.SetTrigger(Eat));
            }

            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                DOTween.Kill(transform.position);
                transform.DOMove(m_top.transform.position, 0.5f)
                         .OnComplete(() => m_aimator.SetTrigger(Eat));
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                m_aimator.SetTrigger(Block);
            }
        }
    }
}