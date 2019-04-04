using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrocoBrush.UI.Menu
{
    /// <summary>
    /// Simple script to expose SceneManager.LoadScene(int) to Unity Event.
    /// How to Use:
    /// - Attach this script to a game object with a Button.
    /// - Add event to the button.
    /// - Drag the SceneLoader component to the event.
    /// - Choose SceneLoader/LoadScene(int).
    /// - Enter Scene build index to the appearing field.
    /// The Scene build index can be seen in "File/Build Settings" under the "Scenes In Build".
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        /// <summary>
        /// Load scene with the given index.
        /// </summary>
        /// <param name="index">Scene Build Index</param>
        public void LoadScene(int index)
        {
            //Clear the current event manager.
            EventManager.Instance.Clear();
            //Load Scene with the given index.
            SceneManager.LoadScene(index);
        }

        /// <summary>
        /// Reloads the current scene.
        /// </summary>
        public void ReloadCurrentScene()
        {
            //Clear the current event manager.
            EventManager.Instance.Clear();
            //Reload the current scene.
            LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}