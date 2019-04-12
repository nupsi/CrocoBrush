using UnityEngine;

namespace CrocoBrush.Events
{
    [RequireComponent(typeof(BoxCollider))]
    public class ClickEvent : MonoBehaviour
    {
        [SerializeField]
        private CustomEvent m_onMouseUp;

        private void OnMouseUp()
        {
            m_onMouseUp.Invoke();
        }
    }
}