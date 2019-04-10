using UnityEngine;

namespace CrocoBrush.Events
{
    [RequireComponent(typeof(BoxCollider))]
    public class ClickEvent : MonoBehaviour
    {
        [SerializeField]
        private CustomEvent m_event;

        private void OnMouseDown()
        {
            m_event.Invoke();
        }
    }
}