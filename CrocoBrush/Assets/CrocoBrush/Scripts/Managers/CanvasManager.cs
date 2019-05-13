using UnityEngine;

namespace CrocoBrush.UI
{
    /// <summary>
    /// Manger to active and deactive different canvases by name.
    /// Add RegisteredCanvas to a game object with Canvas component
    /// to add it to the Canvas Manager.
    /// Use Show(name) to active canvas.
    /// </summary>
    public class CanvasManager : GenericManager<RegisteredCanvas, string>
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Current Canvas Manager Instance.
        /// </summary>
        private static CanvasManager m_instance;

        /*
         * Functions.
         */

        public CanvasManager() : base()
        {
            if(m_instance != null)
            {
                Debug.LogError("Canvas Manager Instance already exists!");
                return;
            }
            m_instance = this;
        }

        public override void RegisterComponent(RegisteredCanvas component)
        {
            base.RegisterComponent(component);
            //Hide the component once it is registered.
            component.Show(false);
        }

        protected override void ProcessComponents(string data)
        {
            m_components.ForEach((component) => component.Show(component.Name == data));
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