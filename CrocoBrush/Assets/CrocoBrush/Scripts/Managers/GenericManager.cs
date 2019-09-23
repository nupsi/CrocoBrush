using System.Collections.Generic;

namespace CrocoBrush
{
    /// <summary>
    /// Base class to create a manager that you can add and remove components.
    /// The Activate function will determinate what the activation does.
    /// </summary>
    /// <typeparam name="T">What the manager manages.</typeparam>
    /// <typeparam name="U">What is used to change the manager.</typeparam>
    public abstract class GenericManager<T, U>
    {
        /// <summary>
        /// List of registered components.
        /// </summary>
        protected List<T> m_components;

        protected GenericManager()
        {
            m_components = new List<T>();
        }

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

        /// <summary>
        /// Calls process components with the given target.
        /// again with Back().
        /// </summary>
        /// <param name="target">Target to process.</param>
        public virtual void Show(U target)
        {
            ProcessComponents(target);
        }

        /// <summary>
        /// Process components stored in m_components.
        /// </summary>
        /// <param name="data">Target component name.</param>
        protected abstract void ProcessComponents(U data);
    }
}