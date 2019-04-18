using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Base class for registered components.
    /// Starts and stops listening to the given events in the event manager.
    /// Use Actions property to initialize m_actions dictionary which contains
    /// the event names as keys and called functions as values.
    /// </summary>
    public abstract class RegisteredBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Dictionary for all the events this component wants to listen.
        /// </summary>
        protected Dictionary<string, Action> m_actions;

        protected virtual void OnEnable()
        {
            //Start listening to each trigger.
            foreach(var pair in Actions)
            {
                EventManager.Instance.StartListening(pair.Key, pair.Value);
            }
        }

        protected virtual void OnDisable()
        {
            //Stop listening to each trigger.
            foreach(var pair in Actions)
            {
                EventManager.Instance.StopListening(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// Dictionary for all the events this component wants to listen.
        /// </summary>
        protected abstract Dictionary<string, Action> Actions { get; }
    }
}