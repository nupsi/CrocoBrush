using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrocoBrush.UI
{
    /// <summary>
    /// Simple script for controlling Main Menu.
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// Load scene with the given index.
        /// </summary>
        /// <param name="scene">Scene index.</param>
        public void Play(int scene)
        {
            SceneManager.LoadScene(scene);
        }

        /// <summary>
        /// Close the application.
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }
    }
}