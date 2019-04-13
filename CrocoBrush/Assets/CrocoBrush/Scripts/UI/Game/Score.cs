using TMPro;
using UnityEngine;

namespace CrocoBrush.UI.Game
{
    /// <summary>
    /// GUI Component to display Crocodiles Score and Anger.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Score : GUIGame
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Cached Text Mesh Pro Text Field component.
        /// </summary>
        private TextMeshProUGUI m_text;

        /*
         * Mono Behaviour Functions.
         */

        protected void Awake()
        {
            m_text = GetComponent<TextMeshProUGUI>();
        }

        /*
         * Functions.
         */

        /// <summary>
        /// Update Text Field component to display latest score and anger.
        /// </summary>
        public void UpdateScore() => m_text.SetText($"Score: {CurrentScore}\nAnger: {CurrentAnger}");

        protected override void UpdateComponent() => UpdateScore();

        protected override void ResetComponent() => UpdateScore();

        /*
         * Accessors.
         */

        /// <summary>
        /// Accessor to get the Score from Crocodiles singleton.
        /// </summary>
        /// <value>The current Score.</value>
        private int CurrentScore => Crocodile.Instance.Score;

        /// <summary>
        /// Accessor to get the Anger from Crocodiles singleton.
        /// </summary>
        /// <value>The current Anger.</value>
        private int CurrentAnger => Crocodile.Instance.Anger;
    }
}