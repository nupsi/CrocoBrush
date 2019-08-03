using UnityEngine;

namespace CrocoBrush.Events
{
    [RequireComponent(typeof(BoxCollider))]
    public class ClickEvent : MonoBehaviour
    {
        [SerializeField]
        private CustomEvent m_onMouseUp;

        protected void OnMouseUp()
        {
            m_onMouseUp.Invoke();
        }
    }
}