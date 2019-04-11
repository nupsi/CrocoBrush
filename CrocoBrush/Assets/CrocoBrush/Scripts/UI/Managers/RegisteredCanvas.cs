using UnityEngine;

namespace CrocoBrush.UI
{
    [RequireComponent(typeof(Canvas))]
    public class RegisteredCanvas : MonoBehaviour
    {
        [SerializeField]
        private string m_name = "None";

        private Canvas m_canvas;

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

        public void Show(bool show)
        {
            m_canvas.enabled = show;
        }

        public string Name => m_name;
    }
}