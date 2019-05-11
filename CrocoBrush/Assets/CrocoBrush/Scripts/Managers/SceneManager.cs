using CrocoBrush.UI;
using UnityEngine;

namespace CrocoBrush.Managers
{
    public class SceneManager : GenericManager<FakeScene, FakeSceneData>
    {
        private static SceneManager m_instance;

        public SceneManager() : base()
        {
            if(m_instance != null)
            {
                Debug.LogError("Scene Manager Instance Already exists!");
                return;
            }
            m_track = true;
            m_instance = this;
        }

        public override void RegisterComponent(FakeScene component)
        {
            base.RegisterComponent(component);
            component.Process("");
        }

        protected override void ProcessComponents(FakeSceneData data)
        {
            Debug.Log($"Active scene: '{data.Scene}'");
            CanvasManager.Instance.Show(data.Canvas);
            CameraManager.Instance.Show(data.Position);
            m_components.ForEach((component) => component.Process(data.Scene));
        }

        protected override bool UsePrevious(FakeSceneData value)
        {
            return value == null;
        }

        public static SceneManager Instance => m_instance ?? new SceneManager();
    }
}