using UnityEngine;

namespace CrocoBrush.Tutorial
{
    [RequireComponent(typeof(Activator))]
    public class TutorialController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] m_parts;

        private int m_current;
        private Activator m_activator;

        protected void Awake()
        {
            m_activator = GetComponent<Activator>();
        }

        protected void OnEnable()
        {
            StartTutorial();
        }

        public void StartTutorial()
        {
            m_current = 0;
            Show();
        }

        public void Next()
        {
            m_current++;
            Show();
        }

        public void ActivateActivator()
        {
            m_activator.Activate();
            LevelController.Instance.PlaySelectedLevel();
            StartTutorial();
        }

        private void Show()
        {
            Hide();
            if(m_current < m_parts.Length)
            {
                m_parts[m_current].SetActive(true);
            }
            else
            {
                ActivateActivator();
            }
        }

        private void Hide()
        {
            foreach(var part in m_parts)
            {
                if(part.activeInHierarchy)
                {
                    part.SetActive(false);
                }
            }
        }
    }
}