using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush.UI.Game
{
    /// <summary>
    /// Display latest score change quality.
    /// </summary>
    public class DisplayQuality : GUIGame
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Tween duration for displayed text.
        /// </summary>
        private float m_duration = 0.6f;

        /// <summary>
        /// Last anger displayed.
        /// Used to track change.
        /// </summary>
        private int m_anger;

        /// <summary>
        /// Last score displayed.
        /// Used to track change.
        /// </summary>
        private int m_score;

        /// <summary>
        /// Object pool for text fields used to display the change qualities.
        /// </summary>
        private Queue<QualityText> m_texts;

        /// <summary>
        /// Queue for active texts.
        /// </summary>
        private Queue<QualityText> m_active;

        /*
         * Mono Behaviour Functions.
         */

        protected void Awake()
        {
            CreateTextPool();
        }

        /*
         * Functions.
         */

        protected override void UpdateComponent()
        {
            //Check if the score has changed.
            if(Crocodile.Score != m_score)
            {
                //Display Text based on the score change.
                DisplayScore(Crocodile.Score - m_score);
                //Update the stored score.
                m_score = Crocodile.Score;
            }

            //Check if the anger has changed.
            if(m_anger < Crocodile.Anger)
            {
                //Display miss text.
                DisplayMiss();
            }
            m_anger = Crocodile.Anger;
        }

        protected override void ResetComponent()
        {
            m_score = Crocodile.Score;
            m_anger = Crocodile.Anger;
            for(int i = 0; i < m_active.Count; i++)
            {
                var text = m_active.Dequeue();
                text.gameObject.SetActive(false);
                m_texts.Enqueue(text);
            }
        }

        /// <summary>
        /// Create object pool for the text fields, that are used to display the qualities.
        /// </summary>
        private void CreateTextPool()
        {
            m_texts = new Queue<QualityText>();
            m_active = new Queue<QualityText>();
            for(int i = 0; i < 8; i++)
            {
                var go = new GameObject($"Text {i}", typeof(RectTransform));
                go.transform.SetParent(transform);
                go.transform.position = transform.position;
                var text = go.AddComponent<QualityText>();
                text.gameObject.SetActive(false);
                m_texts.Enqueue(text);
            }
        }

        /// <summary>
        /// Displays the given quality to the User with a text field.
        /// </summary>
        /// <param name="quality">Quality to display.</param>
        private void DisplayText(Quality quality)
        {
            //Check if the object pool has available text fields.
            if(m_texts.Count == 0)
            {
                return;
            }

            //Get a free text field from the object pool.
            var text = m_texts.Dequeue();
            //Set the text field active.
            text.gameObject.SetActive(true);
            m_active.Enqueue(text);
            //Show a text based on the given quality.
            switch(quality)
            {
                case Quality.Bad:
                    text.DisplayText(this, "Miss", m_duration, Color.red);
                    return;

                case Quality.Good:
                    text.DisplayText(this, "Great", m_duration, Color.blue);
                    return;

                case Quality.Perfect:
                    text.DisplayText(this, "Perfect", m_duration, Color.green);
                    return;
            }
        }

        /// <summary>
        /// Displays a Quality based on the given score.
        /// </summary>
        /// <param name="ammount">Ammount to display.</param>
        private void DisplayScore(int ammount)
        {
            DisplayText(ammount >= 2 ? Quality.Perfect : Quality.Good);
        }

        /// <summary>
        /// Display a miss.
        /// </summary>
        private void DisplayMiss()
        {
            DisplayText(Quality.Bad);
        }

        /// <summary>
        /// Adds the given text field back to the object pool.
        /// </summary>
        /// <param name="text">Text.</param>
        public void AddToPool(QualityText text)
        {
            m_active.Dequeue();
            text.gameObject.SetActive(false);
            m_texts.Enqueue(text);
        }

        /*
         * Accessors.
         */

        /// <summary>
        /// Current Crocodile Instance.
        /// </summary>
        /// <value>Current crocodile Instance.</value>
        private Crocodile Crocodile => Crocodile.Instance;
    }
}