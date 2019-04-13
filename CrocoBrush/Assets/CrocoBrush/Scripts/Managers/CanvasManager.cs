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

        protected override void ProcessComponents(string name)
        {
            m_components.ForEach((component) => 
                component.Show(component.Name == name));
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