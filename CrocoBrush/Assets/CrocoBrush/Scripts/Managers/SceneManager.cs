using System.Collections.Generic;
using CrocoBrush.UI;
using UnityEngine;

namespace CrocoBrush.Managers
{
    /// <summary>
    /// Fake scene manager to make linear scene changes in the game.
    /// Requires Canvas and Camera managers for GUI and camera movement.
    /// Add Fake Scene to a game object. It gets enabled/disabled when that fake scene is active.
    /// You can change scene with Show(scene data), where scene data contains scene name, canvas 
    /// and camera position names.
    /// </summary>
    public class SceneManager : GenericManager<FakeScene, FakeSceneData>
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Current Scene Manager Instance.
        /// </summary>
        private static SceneManager m_instance;

        /// <summary>
        /// History of show calls.
        /// You can use Back() to 'undo' the previous call.
        /// </summary>
        protected Stack<FakeSceneData> m_history;

        /*
         * Functions.
         */

        public SceneManager() : base()
        {
            if(m_instance != null)
            {
                Debug.LogError("Scene Manager Instance Already exists!");
                return;
            }

            m_history = new Stack<FakeSceneData>();
            m_instance = this;
        }

        public override void RegisterComponent(FakeScene component)
        {
            base.RegisterComponent(component);
            component.Process("");
        }

        protected override void ProcessComponents(FakeSceneData data)
        {
            CanvasManager.Instance.Show(data.Canvas);
            CameraManager.Instance.Show(data.Position);
            m_components.ForEach((component) => component.Process(data.Scene));
        }

        public override void Show(FakeSceneData target)
        {
            m_history.Push(UsePrevious(target) ? m_history.Peek() : target);
            base.Show(target);
        }

        /// <summary>
        /// Show the previous call again.
        /// </summary>
        public virtual void Back()
        {
            m_history.Pop();
            Show(m_history.Pop());
        }

        /// <summary>
        /// Is the previous entry dublicated to the history or the new one added.
        /// </summary>
        /// <returns>Should the previous value be used.</returns>
        /// <param name="value">New Fake Scene data.</param>
        protected bool UsePrevious(FakeSceneData value)
        {
            return value == null;
        }

        /*
         * Accessors.
         */

        /// <summary>
        /// Current singleton instance.
        /// </summary>
        /// <value>Current instance.</value>
        public static SceneManager Instance => m_instance ?? new SceneManager();
    }
}