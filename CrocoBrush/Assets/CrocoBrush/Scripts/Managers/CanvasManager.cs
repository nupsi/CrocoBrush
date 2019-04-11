using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush.UI
{
    /// <summary>
    /// Manger to active and deactive different canvases by name.
    /// Add RegisteredCanvas to a game object with Canvas component
    /// to add it to the Canvas Manager. 
    /// Use Activate(name) to active canvas.
    /// Active the previous canvas with Back().
    /// </summary>
    public class CanvasManager : GenericManager<RegisteredCanvas>
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Current Canvas Manager Instance.
        /// </summary>
        private static CanvasManager m_instance;

        /// <summary>
        /// Stack of previous activate calls.
        /// This is used to go back.
        /// </summary>
        private Stack<string> m_history;

        /*
         * Functions.
         */

        public CanvasManager()
        {
            if(m_instance != null)
            {
                Debug.LogError("Canvas Manager Instance already exists!");
                return;
            }
            m_instance = this;
            m_components = new List<RegisteredCanvas>();
            m_history = new Stack<string>();
        }

        public override void RegisterComponent(RegisteredCanvas component)
        {
            base.RegisterComponent(component);
            //Hide the component once it is registered.
            component.Show(false);
        }

        /// <summary>
        /// Show canvases that have the given name.
        /// </summary>
        /// <param name="name">Name to display.</param>
        public void ShowCanvas(string name)
        {
            m_history.Push(name);
            m_components.ForEach((c) => c.Show(c.Name == name));
            CameraManager.Instance.MoveToPosition(name);
        }

        /// <summary>
        /// Active the previous active canvas.
        /// </summary>
        public void Back()
        {
            m_history.Pop();
            ShowCanvas(m_history.Pop());
        }

        /*
         * Accessors.
         */

        /// <summary>
        /// Current Canvas Manager Instance.
        /// </summary>
        public static CanvasManager Instance => m_instance ?? new CanvasManager();
    }
}