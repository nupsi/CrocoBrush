using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Manager for different camera positions.
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
            m_track = false;
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