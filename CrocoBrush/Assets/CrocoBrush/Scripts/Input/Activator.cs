using CrocoBrush.Managers;
using UnityEngine;

namespace CrocoBrush
{
    public class Activator : MonoBehaviour
    {
        [SerializeField]
        private bool m_separate;

        [SerializeField]
        private string m_name;

        [SerializeField]
        private string m_canvas;

        [SerializeField]
        private string m_position;

        /// <summary>
        /// Activate the target scene.
        /// </summary>
        public void Activate() => SceneManager.Instance.Show(SceneData);

        /// <summary>
        /// Go back to the previous state.
        /// </summary>
        public void Back() => SceneManager.Instance.Back();

        /// <summary>
        /// Target scene data.
        /// </summary>
        public FakeSceneData SceneData => m_separate
            ? new FakeSceneData(m_canvas, m_canvas, m_position)
            : new FakeSceneData(m_name);
    }
}