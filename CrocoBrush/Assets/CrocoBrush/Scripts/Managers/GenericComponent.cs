using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Generic component that is added and removed to and from generic manager.
    /// </summary>
    public abstract class GenericComponent<T, U> : MonoBehaviour
    {
        /// <summary>
        /// Name to identify the component.
        /// </summary>
        [SerializeField]
        protected string m_name = "None";

        public void OnEnable()
        {
            Manager.RegisterComponent(Component);
        }

        public void OnDisable()
        {
            Manager.RemoveComponent(Component);
        }

        /// <summary>
        /// Property for the current component.
        /// This should usually just be 'Component => this', since T should be managed type
        /// and the current class should be what is managed.
        /// </summary>
        /// <value>Component to add and remove.</value>
        protected abstract T Component { get; }

        /// <summary>
        /// Target manager for the components.
        /// This should point to a manager singleton.
        /// </summary>
        /// <value>Manager where this component is added.</value>
        public abstract GenericManager<T, U> Manager { get; }
    }
}