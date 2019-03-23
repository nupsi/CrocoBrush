using UnityEngine;

namespace CrocoBrush.UI
{
    public abstract class GUIBehaviour : MonoBehaviour, IGUI
    {
        protected virtual void Awake()
        {
            GUIController.Instance.ReqisterComponent(this);
        }

        public abstract void RequestUpdate();
    }
}