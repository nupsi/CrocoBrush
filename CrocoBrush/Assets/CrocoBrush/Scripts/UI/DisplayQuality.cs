using UnityEngine;
using System.Collections.Generic;

namespace CrocoBrush.UI
{
    public class DisplayQuality : GUIBehaviour
    {
        private float m_duration = 0.6f;
        private int m_anger;
        private int m_score;

        private Queue<QualityText> m_texts;

        protected override void Awake()
        {
            base.Awake();
            CreateTextPool();
        }

        public override void RequestUpdate()
        {
            if(Crocodile.Score != m_score)
            {
                DisplayScore(Crocodile.Score - m_score);
                m_score = Crocodile.Score;
            }

            if(Crocodile.Anger != m_anger)
            {
                DisplayMiss();
                m_anger = Crocodile.Anger;
            }
        }

        private void CreateTextPool()
        {
            m_texts = new Queue<QualityText>();
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

        private void DisplayText(Quality quality)
        {
            if(m_texts.Count == 0)
            {
                return;
            }

            var text = m_texts.Dequeue();
            text.gameObject.SetActive(true);
            switch(quality)
            {
                case Quality.Bad:
                    text.DisplayText(this, "Miss", m_duration, Color.red);
                    break;
                case Quality.Avarage:
                    text.DisplayText(this, "Great", m_duration, Color.blue);
                    break;
                case Quality.Good:
                    text.DisplayText(this, "Perfect", m_duration, Color.green);
                    break;
            }
        }

        private void DisplayScore(int ammount)
        {
            DisplayText(ammount == 2 ? Quality.Good : Quality.Avarage);
        }

        private void DisplayMiss()
        {
            DisplayText(Quality.Bad);
        }

        public void AddToPool(QualityText text)
        {
            text.gameObject.SetActive(false);
            m_texts.Enqueue(text);
        }

        private Crocodile Crocodile => Crocodile.Instance;
    }
}