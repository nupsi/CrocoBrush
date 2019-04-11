using UnityEngine;

namespace CrocoBrush.UI
{
    /// <summary>
    /// Exposes canvas manager functions.
    /// Call the functions throuh Unity events.
    /// </summary>
    public class ActivateCanvas : MonoBehaviour
    {
        /// <summary>
        /// Show canvas with the given name.
        /// </summary>
        /// <param name="name">Target canvas name.</param>
        public void ShowCanvas(string name)
        {
            CanvasManager.Instance.ShowCanvas(name);
        }

        /// <summary>
        /// Go back to the previous active canvas.
        /// </summary>
        public void Back() => CanvasManager.Instance.Back();
    }
}