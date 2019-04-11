using UnityEngine;

namespace CrocoBrush.UI
{
    public class ActivateCanvas : MonoBehaviour
    {
        public void Activate(string name)
        {
            CanvasManager.Instance.Activate(name);
        }

        public void Back() => CanvasManager.Instance.Back();
    }
}