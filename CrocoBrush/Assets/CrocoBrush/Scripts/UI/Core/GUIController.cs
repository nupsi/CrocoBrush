using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Singleton class for controlling IGUI components.
    /// You can use GUIController.Instance.RegisterComponent(IGUI) to register component.
    /// You can use GUIController.Instance.UpdateComponents() to update registered components.
    /// </summary>
    public class GUIController
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Private instance so whe can create the instance in the accessor with lazy loading.
        /// </summary>
        private static GUIController m_instance;

        /// <summary>
        /// List of registered components.
        /// </summary>
        private List<IGUI> m_components;

        /*
         * Functions.
         */

        /// <summary>
        /// Initializes a new instance of the class if it doesn't already exists.
        /// </summary>
        public GUIController()
        {
            if(m_instance != null)
            {
                Debug.Log("GUI Controller Instance already exists!");
                return;
            }
            m_components = new List<IGUI>();
            m_instance = this;
        }

        /// <summary>
        /// Register given component.
        /// </summary>
        /// <param name="component">Component to register.</param>
        public void RegisterComponent(IGUI component)
        {
            m_components.Add(component);
        }

        /// <summary>
        /// Removes given component from registered components.
        /// </summary>
        /// <param name="component">Component to remove.</param>
        public void RemoveComponent(IGUI component)
        {
            m_components.Remove(component);
        }

        /// <summary>
        /// Update all registered components.
        /// </summary>
        public void UpdateComponents() => m_components?.ForEach((component) => component.RequestUpdate());

        /// <summary>
        /// Clears the current GUI Controller Instance.
        /// It is safer to clear the instance, before loading a new scene to make sure that no
        /// previous components are registered. Components should however register and remove
        /// themselves on enable and disable.
        /// </summary>
        public void Clear() => m_instance = null;

        /*
         * Accessors.
         */

        /// <summary>
        /// Current GUIController Instance.
        /// Initialize new Intsance if doesn't exists.
        /// </summary>
        /// <value>Current GUIController Instance.</value>
        public static GUIController Instance => m_instance ?? new GUIController();
    }
}