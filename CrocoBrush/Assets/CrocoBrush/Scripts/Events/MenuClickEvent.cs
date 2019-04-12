using UnityEngine;
using UnityEngine.EventSystems;

namespace CrocoBrush.Events
{
    [RequireComponent(typeof(BoxCollider))]
    public class MenuClickEvent : MonoBehaviour
    {
        [SerializeField]
        private CustomEvent m_onMouseUp;

        private BoxCollider m_collider;

        private void Awake() => m_collider = GetComponent<BoxCollider>();

        private void OnEnable() => EventManager.Instance.StartListening("Menu", Enable);

        private void OnDisable() => EventManager.Instance.StopListening("Menu", Enable);

        private void OnMouseUp()
        {
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                m_onMouseUp.Invoke();
            }
        }

        private void Enable() => m_collider.enabled = true;
    }
}