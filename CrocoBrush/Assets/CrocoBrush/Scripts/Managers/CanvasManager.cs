using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush.UI
{
    public class CanvasManager : GenericManager<RegisteredCanvas>
    {
        private static CanvasManager m_instance;
        private Stack<string> m_history;

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
            component.Show(false);
        }

        public override void Activate(string name)
        {
            m_history.Push(name);
            m_components.ForEach((c) => c.Show(c.Name == name));
            CameraManager.Instance.Activate(name);
        }

        public void Back()
        {
            m_history.Pop();
            Activate(m_history.Pop());
        }

        public static CanvasManager Instance => m_instance ?? new CanvasManager();
    }
}