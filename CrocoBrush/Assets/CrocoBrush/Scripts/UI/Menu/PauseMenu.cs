using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrocoBrush.UI.Menu
{
    /// <summary>
    /// Simple script to control pause menu.
    /// </summary>
    public class PauseMenu : MonoBehaviour
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Menu game object.
        /// This object is activated and deactivated to show and hide the menu.
        /// </summary>
        [SerializeField]
        private GameObject m_menu;

        /// <summary>
        /// Components to activate and deactivate on pause and unpause.
        /// </summary>
        [SerializeField]
        private Behaviour[] m_components;

        /// <summary>
        /// Is the game running.
        /// Stored for toggling the pause.
        /// </summary>
        private bool m_running;

        /*
         * Mono Behaviour Functions.
         */

        private void Awake()
        {
            m_running = true;
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

        /*
         * Functions.
         */

        /// <summary>
        /// Unpause the game.
        /// </summary>
        public void Return() => TogglePause(true);

        /// <summary>
        /// Return to Main Menu scene.
        /// </summary>
        public void MainMenu() => SceneManager.LoadScene(0);

        /// <summary>
        /// Toggle pause to give state
        /// </summary>
        /// <param name="running">Set the game to running.</param>
        private void TogglePause(bool running)
        {
            m_menu.SetActive(!running);
            foreach(var component in m_components)
            {
                if(component is AudioSource source)
                {
                    if(running)
                    {
                        source.UnPause();
                    }
                    else
                    {
                        source.Pause();
                    }
                }
                else
                {
                    component.enabled = running;
                }
            }
            Time.timeScale = running ? 1 : 0;
            m_running = running;
        }
    }
}