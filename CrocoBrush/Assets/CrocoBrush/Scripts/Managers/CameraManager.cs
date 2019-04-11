using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    public class CameraManager : GenericManager<CameraPosition>
    {
        private static CameraManager m_instance;

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

        public override void Activate(string name)
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

        public static CameraManager Instance => m_instance ?? new CameraManager();
    }
}