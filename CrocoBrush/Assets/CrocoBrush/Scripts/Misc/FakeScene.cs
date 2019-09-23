using UnityEngine;

namespace CrocoBrush.Managers
{
    /// <summary>
    /// Fake Scene to manage game objects and behaviours in one Unity scene.
    /// </summary>
    public class FakeScene : GenericComponent<FakeScene, FakeSceneData>
    {
        /// <summary>
        /// Array of all the behaviours controlled by this fake scene.
        /// </summary>
        [SerializeField]
        private Behaviour[] m_behaviour;

        /// <summary>
        /// Array of all the game objects controled by this fake scene.
        /// </summary>
        [SerializeField]
        private GameObject[] m_gameObjects;

        /// <summary>
        /// What is the last activation status from process.
        /// </summary>
        private bool m_current = true;

        /// <summary>
        /// Process the fake scene.
        /// Enables/Disables game object and behaviours if the given scene name matches the current scene name.
        /// </summary>
        /// <param name="name">Target fake scene name.</param>
        public void Process(string name)
        {
            //Store the current activation status to temporary variable.
            var isTarget = (m_name == name);
            //Check if the previous activation status matches the new activation status.
            if(m_current == isTarget)
            {
                //All the components should have the target activation status already and nothing needs to be done.
                return;
            }

            //Go through all the registered behaviours and set the enabled status to the current activation status.
            foreach(var behaviour in m_behaviour)
            {
                behaviour.enabled = isTarget;
            }

            //Go through all the registered game objects and enable them based on the current activation status.
            foreach(var gameObject in m_gameObjects)
            {
                gameObject.SetActive(isTarget);
            }

            //Set the current activation status.
            m_current = isTarget;
        }

        /// <summary>
        /// Current Fake Scene object.
        /// </summary>
        protected override FakeScene Component => this;

        /// <summary>
        /// Fake Scene Manager Instance.
        /// </summary>
        public override GenericManager<FakeScene, FakeSceneData> Manager => SceneManager.Instance;
    }
}