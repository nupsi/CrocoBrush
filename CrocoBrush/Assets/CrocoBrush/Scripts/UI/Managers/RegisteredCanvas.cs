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
            CanvasManager.Instance.RegisterCanvas(this);
        }

        public void OnDisable()
        {
            CanvasManager.Instance.RemoveCanvas(this);
        }

        public void Show(bool show)
        {
            m_canvas.enabled = show;
            ShowChild(show);
        }

        private void ShowChild(bool show)
        {
            foreach(Transform child in transform)
            {
                print(child);
                child.gameObject.SetActive(show);
            }
        }

        public string Name => m_name;
    }
}