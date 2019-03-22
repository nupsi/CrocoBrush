using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrocoBrush.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_menu;

        [SerializeField]
        private Behaviour[] m_components;

        private bool m_running;

        private void Awake()
        {
            m_running = true;
            m_menu.SetActive(false);
        }

        private void Update()
        {
            if(Input.GetButtonDown("Cancel"))
            {
                TogglePause(!m_running);
            }
        }

        private void OnDestroy()
        {
            Time.timeScale = 1;
        }

        public void Return() => TogglePause(true);

        public void MainMenu() => SceneManager.LoadScene(0);

        private void TogglePause(bool running)
        {
            m_menu.SetActive(!running);
            foreach(var component in m_components)
            {
                component.enabled = running;
            }
            Time.timeScale = running ? 1 : 0;
            m_running = running;
        }
    }
}