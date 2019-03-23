using UnityEngine;
using TMPro;

namespace CrocoBrush.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Score : GUIBehaviour
    {
        private TextMeshProUGUI m_text;

        protected override void Awake()
        {
            base.Awake();
            m_text = GetComponent<TextMeshProUGUI>();
        }

        public override void RequestUpdate() => UpdateScore();

        public void UpdateScore() => m_text.SetText($"Score: {CurrentScore}\nAnger: {CurrentAnger}");

        private int CurrentScore => Crocodile.Instance.Score;

        private int CurrentAnger => Crocodile.Instance.Anger;
    }
}