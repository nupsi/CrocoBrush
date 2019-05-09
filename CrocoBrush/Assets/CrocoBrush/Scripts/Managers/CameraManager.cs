using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Manager for different camera positions.
    /// </summary>
    public class CameraManager : GenericManager<CameraPosition>
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

        protected override void ProcessComponents(string name)
        {
            foreach(var component in m_components)
            {
                if(component.Name == name)
                {
                    component.SetCamera(Camera.main);
                }
                else
                {
                    component.Deactive();
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