using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Manager for different camera positions.
    /// Add Camera Position to a game object and give the component a name.
    /// Call Show(component name) to move the camera to the location.
    /// </summary>
    public class CameraManager : GenericManager<CameraPosition, string>
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Current Camera Manager Instance.
        /// </summary>
        private static CameraManager m_instance;

        /*
         * Functions.
         */

        public CameraManager() : base()
        {
            if(m_instance != null)
            {
                Debug.LogError("Camera Manager Instance already exists!");
                return;
            }
            m_instance = this;
        }

        protected override void ProcessComponents(string data)
        {
            foreach(var component in m_components)
            {
                if(component.Name == data)
                {
                    component.SetCamera(Camera.main);
                    return;
                }
            }
        }

        /*
         * Accessors.
         */

        /// <summary>
        /// Current Camera Manager Instance.
        /// </summary>
        public static CameraManager Instance => m_instance ?? new CameraManager();
    }
}