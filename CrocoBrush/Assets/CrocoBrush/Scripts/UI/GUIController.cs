using UnityEngine;
using System.Collections.Generic;

namespace CrocoBrush
{
    public class GUIController
    {
        private static GUIController m_instance;

        private List<IGUI> m_components;

        public GUIController()
        {
            if(m_instance != null)
            {
                Debug.Log("GUI Controller Instance already exists!");
                return;
            }
            m_components = new List<IGUI>();
            m_instance = this;
        }

        public void ReqisterComponent(IGUI component)
        {
            m_components.Add(component);
        }

        public void UpdateComponents() => m_components.ForEach((component) => component.RequestUpdate());

        public static GUIController Instance => m_instance ?? new GUIController();
    }
}