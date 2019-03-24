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
            GUIController.Instance.RegisterComponent(this);
            m_rect = GetComponent<RectTransform>();
        }

        public abstract void RequestUpdate();
    }
}