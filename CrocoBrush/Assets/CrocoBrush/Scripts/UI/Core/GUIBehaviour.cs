using UnityEngine;

namespace CrocoBrush.UI
{
    /// <summary>
    /// Abstract class for creating registered GUI component.
    /// The component is registered on Awake and GUIController.UpdateComponents()
    /// can be used to update all the registered components.
    /// </summary>
    public abstract class GUIBehaviour : MonoBehaviour, IGUI
    {
        protected RectTransform m_rect;

        protected virtual void Awake()
        {
            m_rect = GetComponent<RectTransform>();
        }

        protected virtual void OnEnable()
        {
            GUIController.Instance.RegisterComponent(this);
        }

        protected virtual void OnDisable()
        {
            GUIController.Instance.RemoveComponent(this);
        }

        public abstract void RequestUpdate();
    }
}