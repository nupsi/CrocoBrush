using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Input reader for the spacebar mechanic.
    /// </summary>
    public class SpacebarInput : MonoBehaviour
    {
        /// <summary>
        /// Scriptable keybind for the spacebar mechanic.
        /// </summary>
        [SerializeField]
        private ScriptableKeybind m_keybind;

        protected void Update()
        {
            if(m_keybind.GetKeyDown())
            {
                if(Spacebar.Instance.Visible)
                {
                    Spacebar.Instance.ClearSpace();
                }
                else
                {
                    Crocodile.Instance.AddScore(Quality.Bad);
                }
            }
        }
    }
}