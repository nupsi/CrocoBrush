using CrocoBrush.UI;
using UnityEngine;

namespace CrocoBrush
{
    public class Activator : MonoBehaviour
    {
        [SerializeField]
        private bool m_separate;

        [SerializeField]
        private string m_name;

        [SerializeField]
        private string m_canvas;

        [SerializeField]
        private string m_position;

        public void Activate()
        {
            ShowCanvas();
            MoveTo();
        }

        /// <summary>
        /// Show canvas with the given name.
        /// </summary>
        /// <param name="name">Target canvas name.</param>
        public void ShowCanvas() =>
            CanvasManager.Instance.ShowCanvas(m_separate ? m_canvas : m_name);

        /// <summary>
        /// Move camera to a point with the given name.
        /// </summary>
        /// <param name="name">Target position name.</param>
        public void MoveTo() =>
            CameraManager.Instance.MoveToPosition(m_separate ? m_position : m_name);

        /// <summary>
        /// Go back to the previous state.
        /// </summary>
        public void Back()
        {
            CanvasManager.Instance.Back();
            CameraManager.Instance.Back();
        }
    }
}