using UnityEngine;
using UnityEngine.EventSystems;

namespace CrocoBrush.Events
{
    [RequireComponent(typeof(BoxCollider))]
    public class MenuClickEvent : MonoBehaviour
    {
        [SerializeField]
        private CustomEvent m_onMouseUp;

        protected void OnMouseUp()
        {
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                m_onMouseUp.Invoke();
            }
        }
    }
}