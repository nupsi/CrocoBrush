using UnityEngine;

namespace CrocoBrush.Managers
{
    public class FakeScene : MonoBehaviour
    {
        [SerializeField]
        private string m_name = "None";

        [SerializeField]
        private Behaviour[] m_behaviour;

        [SerializeField]
        private GameObject[] m_gameObjects;

        private bool m_current = true;

        public void OnEnable() => SceneManager.Instance.RegisterComponent(this);

        public void OnDisable() => SceneManager.Instance.RemoveComponent(this);

        public void Process(string name)
        {
            var isTarget = (m_name == name);
            if(m_current == isTarget)
            {
                return;
            }

            foreach(var behaviour in m_behaviour)
            {
                behaviour.enabled = isTarget;
            }

            foreach(var gameObject in m_gameObjects)
            {
                gameObject.SetActive(isTarget);
            }

            m_current = isTarget;
        }
    }
}