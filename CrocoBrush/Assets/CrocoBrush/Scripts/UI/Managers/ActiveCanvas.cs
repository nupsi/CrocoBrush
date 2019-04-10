using UnityEngine;

namespace CrocoBrush.UI
{
    public class ActiveCanvas : MonoBehaviour
    {
        public void Activate(string name)
        {
            CanvasManager.Instance.ActivateCanvas(name);
        }
    }
}