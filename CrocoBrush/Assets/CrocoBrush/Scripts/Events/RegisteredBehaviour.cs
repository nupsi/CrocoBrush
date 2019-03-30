using UnityEngine;

namespace CrocoBrush
{
    public abstract class RegisteredBehaviour : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            EventManager.Instance.StartListening(EventName, UpdateComponent);
        }

        protected virtual void OnDisable()
        {
            EventManager.Instance.StopListening(EventName, UpdateComponent);
        }

        protected abstract void UpdateComponent();

        protected abstract string EventName { get; }
    }
}