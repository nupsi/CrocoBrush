using System.Collections.Generic;

namespace CrocoBrush
{
    /// <summary>
    /// Base class to create a manager that you can add and remove components.
    /// The Activate function will determinate what the activation does.
    /// </summary>
    /// <typeparam name="T">What the manager manages.</typeparam>
    public abstract class GenericManager<T>
    {
        /// <summary>
        /// List of registered components.
        /// </summary>
        protected List<T> m_components;

        /// <summary>
        /// Register component.
        /// </summary>
        /// <param name="component">Component to register.</param>
        public virtual void RegisterComponent(T component) => m_components.Add(component);

        /// <summary>
        /// Remove registered component.
        /// </summary>
        /// <param name="component">Component to remove.</param>
        public virtual void RemoveComponent(T component) => m_components.Remove(component);
    }
}