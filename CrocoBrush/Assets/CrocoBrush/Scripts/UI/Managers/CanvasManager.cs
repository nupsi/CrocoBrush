using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush.UI
{
    public class CanvasManager
    {
        private static CanvasManager m_instance;
        private List<RegisteredCanvas> m_components;

        public CanvasManager()
        {
            if(m_instance != null)
            {
                Debug.LogError("Canvas Manager Instance already exists!");
                return;
            }
            m_instance = this;
            m_components = new List<RegisteredCanvas>();
        }

        public void RegisterCanvas(RegisteredCanvas canvas)
        {
            m_components.Add(canvas);
            canvas.Show(false);
        }

        public void RemoveCanvas(RegisteredCanvas canvas)
        {
            m_components.Remove(canvas);
        }

        public void ActivateCanvas(string name)
        {
            m_components.ForEach((c) => c.Show(c.Name == name));
        }

        public static CanvasManager Instance => m_instance ?? new CanvasManager();
    }
}