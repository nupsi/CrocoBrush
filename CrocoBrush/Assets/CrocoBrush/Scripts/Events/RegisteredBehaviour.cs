using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Base class for registered components.
    /// 
    /// This components listens to a given event in the Event Manager
    /// and triggers the UpdateComponent function when the Event Manager
    /// receives a trigger request for the event name.
    /// </summary>
    public abstract class RegisteredBehaviour : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            //Start listening to a event, when the game object is enabled.
            EventManager.Instance.StartListening(EventName, UpdateComponent);
        }

        protected virtual void OnDisable()
        {
            //Stop listening to a event, when the game object is deactivated.
            EventManager.Instance.StopListening(EventName, UpdateComponent);
        }

        /// <summary>
        /// Function that gets called when the event is triggered.
        /// </summary>
        protected abstract void UpdateComponent();

        /// <summary>
        /// Event name to listen to in the Event Manager.
        /// </summary>
        /// <value>Event name to listen to.</value>
        protected abstract string EventName { get; }
    }
}