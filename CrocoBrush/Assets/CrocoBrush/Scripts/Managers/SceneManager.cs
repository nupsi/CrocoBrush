using UnityEngine;

namespace CrocoBrush.Managers
{
    public class SceneManager : GenericManager<FakeScene>
    {
        private static SceneManager m_instance;

        public SceneManager() : base()
        {
            if(m_instance != null)
            {
                Debug.LogError("Scene Manager Instance Already exists!");
                return;
            }
            m_instance = this;
        }

        public override void RegisterComponent(FakeScene component)
        {
            base.RegisterComponent(component);
            component.Process("");
        }

        protected override void ProcessComponents(string name)
        {
            m_components.ForEach((component) => component.Process(name));
        }

        public static SceneManager Instance => m_instance ?? new SceneManager();
    }
}