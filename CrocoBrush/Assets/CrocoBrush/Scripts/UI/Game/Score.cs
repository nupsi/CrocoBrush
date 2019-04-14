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
        /// Update Text Field component to display the latest properties.
        /// </summary>
        public void UpdateScore()
        {
            var text = string.Format(
                "Score: {0}\nAnger: {1}\nStreak: {2}\nBest Streak: {3}\nPerfect: {4}\nGreat: {5}\nBad: {6}", 
                CurrentScore, 
                Anger, 
                Streak, 
                BestStreak, 
                Perfect, 
                Good, 
                Bad
            );
            m_text.SetText(text);
        }

        protected override void UpdateComponent() => UpdateScore();

        protected override void ResetComponent() => UpdateScore();

        /*
         * Accessors.
         */

        private int CurrentScore => Crocodile.Instance.Score;

        private int Anger => Crocodile.Instance.Anger;

        private int Streak => Crocodile.Instance.Streak;

        private int BestStreak => Crocodile.Instance.BestStreak;

        private int Perfect => Crocodile.Instance.HitCounts[Quality.Perfect];

        private int Good => Crocodile.Instance.HitCounts[Quality.Good];

        private int Bad => Crocodile.Instance.HitCounts[Quality.Bad];
    }
}