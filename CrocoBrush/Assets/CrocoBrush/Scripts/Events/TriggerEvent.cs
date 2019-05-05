using UnityEngine;

namespace CrocoBrush.Events
{
    /// <summary>
    /// Trigger Even Manager events from Unity.
    /// </summary>
    public class TriggerEvent : MonoBehaviour
    {
        /// <summary>
        /// Trigger event with the given name.
        /// </summary>
        /// <param name="name">Event name.</param>
        public void Trigger(string name)
        {
            EventManager.Instance.TriggerEvent(name);
        }
    }
}