using UnityEngine;

namespace CrocoBrush.UI.Menu
{
    /// <summary>
    /// Simple script to control pause menu.
    /// </summary>
    public class PauseMenu : MonoBehaviour
    {
        private void OnEnable()
        {
            LevelController.Instance?.Pause();
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
            LevelController.Instance?.UnPause();
        }
    }
}