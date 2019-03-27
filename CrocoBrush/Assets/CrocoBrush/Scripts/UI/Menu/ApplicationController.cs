using UnityEngine;

namespace CrocoBrush.UI.Menu
{
    /// <summary>
    /// Simple script to expose Application.Quit() to Unity Event.
    /// How to Use:
    /// - Attach this script to a game object with a Button.
    /// - Add event to the button.
    /// - Drag the ApplicationController component to the event.
    /// - Choose ApplicationController/Quit().
    /// </summary>
    public class ApplicationController : MonoBehaviour
    {
        /// <summary>
        /// Sends a request to quit (close) the application.
        /// </summary>
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}