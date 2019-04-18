using UnityEngine;

namespace CrocoBrush.UI
{
    /// <summary>
    /// Canvas that gets added to the Canvas Manager.
    /// Canvas Manager is used to control the visibility.
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class RegisteredCanvas : MonoBehaviour
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Name to identify the component.
        /// </summary>
        [SerializeField]
        private string m_name = "None";

        /// <summary>
        /// Target canvas to enable and disable.
        /// </summary>
        private Canvas m_canvas;

        /*
         * Mono Behaviour functions.
         */

        private void Awake()
        {
            m_canvas = GetComponent<Canvas>();
        }

        public void OnEnable()
        {
            CanvasManager.Instance.RegisterComponent(this);
        }

        public void OnDisable()
        {
            CanvasManager.Instance.RemoveComponent(this);
        }

        /*
         * Functions.
         */

        /// <summary>
        /// Enable the target canvas.
        /// </summary>
        /// <param name="show">Enable the target canvas.</param>
        public void Show(bool show)
        {
            if(m_canvas.enabled != show)
            {
                m_canvas.enabled = show;
            }
        }

        /*
         * Accessors.
         */

        /// <summary>
        /// Name to identify the component.
        /// </summary>
        public string Name => m_name;
    }
}