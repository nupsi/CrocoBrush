using CrocoBrush.Managers;
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
            CanvasManager.Instance.Show(m_separate ? m_canvas : m_name);
            CameraManager.Instance.Show(m_separate ? m_position : m_name);
            SceneManager.Instance.Show(m_separate ? m_canvas : m_name);
        }

        /// <summary>
        /// Go back to the previous state.
        /// </summary>
        public void Back()
        {
            CanvasManager.Instance.Back();
            CameraManager.Instance.Back();
            SceneManager.Instance.Back();
        }
    }
}