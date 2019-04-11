using System.Collections.Generic;

namespace CrocoBrush
{
    public abstract class GenericManager<T>
    {
        protected List<T> m_components;

        public virtual void RegisterComponent(T component) => m_components.Add(component);

        public virtual void RemoveComponent(T component) => m_components.Remove(component);

        public abstract void Activate(string name);
    }
}