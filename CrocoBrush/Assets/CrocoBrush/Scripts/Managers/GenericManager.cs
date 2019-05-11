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

        /// <summary>
        /// Show history.
        /// </summary>
        protected Stack<U> m_history;

        /// <summary>
        /// Keep track of the show history;
        /// </summary>
        protected bool m_track;

        protected GenericManager()
        {
            m_components = new List<T>();
            m_history = new Stack<U>();
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
        /// <param name="target">Target to process.</param>
        public virtual void Show(U target)
        {
            if(m_track)
            {
                m_history.Push(UsePrevious(target) ? m_history.Peek() : target);
            }
            ProcessComponents(target);
        }

        /// <summary>
        /// Should the previously added element be used instead of adding the new one.
        /// </summary>
        /// <returns>Should the previous value be used instead of the new value.</returns>
        protected virtual bool UsePrevious(U value)
        {
            return false;
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
        /// <param name="data">Target component name.</param>
        protected abstract void ProcessComponents(U data);
    }
}