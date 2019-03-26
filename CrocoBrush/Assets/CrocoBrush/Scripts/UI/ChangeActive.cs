using UnityEngine;
using DG.Tweening;

namespace CrocoBrush.UI
{
    /// <summary>
    /// Simple script to activa given object while deactiving the current object.
    /// </summary>
    public class ChangeActive : MonoBehaviour
    {
        /// <summary>
        /// Object to activate on ActiveOther()
        /// </summary>
        [SerializeField]
        private GameObject m_objectToActive;

        /// <summary>
        /// Set the other object active and deactive the current object.
        /// </summary>
        public void ActiveOther()
        {
            m_objectToActive.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}