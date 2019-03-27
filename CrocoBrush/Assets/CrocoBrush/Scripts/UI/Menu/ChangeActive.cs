using UnityEngine;

namespace CrocoBrush.UI.Menu
{
    /// <summary>
    /// Simple script to activa given object while deactiving the current object.
    /// </summary>
    public class ChangeActive : MonoBehaviour
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Game object to deactivate on ActiveOther()
        /// </summary>
        [SerializeField]
        private GameObject m_objectToDeactive;

        /// <summary>
        /// Game object to activate on ActiveOther()
        /// </summary>
        [SerializeField]
        private GameObject m_objectToActive;

        /*
         * Functions.
         */

        /// <summary>
        /// Set the other object active and deactive the current object.
        /// </summary>
        public void ActiveOther()
        {
            m_objectToActive.SetActive(true);
            m_objectToDeactive.SetActive(false);
        }

        /*
         * Accessors.
         */

        /// <summary>
        /// Accessor for the game object to deactivate on ActiveOther()
        /// </summary>
        public GameObject ObjectToDeactive => m_objectToDeactive;

        /// <summary>
        ///  Accessor for the game object to activate on ActiveOther()
        /// </summary>
        public GameObject ObjectToActive => m_objectToActive;
    }
}