using System.Collections.Generic;
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

        public CameraManager()
        {
            if(m_instance != null)
            {
                Debug.LogError("Camera Manager Instance already exists!");
                return;
            }
            m_instance = this;
            m_components = new List<CameraPosition>();
        }

        /// <summary>
        /// Move the camera to a position with the given name.
        /// </summary>
        /// <param name="name">Target position name.</param>
        public void MoveToPosition(string name)
        {
            m_components.ForEach((c) =>
            {
                if(c.Name == name)
                {
                    c.SetCamera(Camera.main);
                    return;
                }
            });
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