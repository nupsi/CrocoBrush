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
        /// Show history.
        /// </summary>
        protected Stack<string> m_history;

        protected GenericManager()
        {
            m_components = new List<T>();
            m_history = new Stack<string>();
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
        /// Calls process components with the given name and adds
        /// it the given name to the history so it can be called
        /// again with Back().
        /// </summary>
        /// <param name="name">Target name to process.</param>
        public virtual void Show(string name)
        {
            m_history.Push((name == string.Empty) ? m_history.Peek() : name);
            ProcessComponents(name);
        }

        /// <summary>
        /// Process the previous name.
        /// </summary>
        public virtual void Back()
        {
            m_history.Pop();
            Show(m_history.Pop());
        }

        /// <summary>
        /// Process components stored in m_components.
        /// </summary>
        /// <param name="name">Target component name.</param>
        protected abstract void ProcessComponents(string name);
    }
}