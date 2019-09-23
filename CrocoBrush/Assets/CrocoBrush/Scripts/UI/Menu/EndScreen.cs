using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CrocoBrush.UI.Menu
{
    public class EndScreen : RegisteredBehaviour
    {
        [Header("Text Fields")]
        [SerializeField]
        private TextMeshProUGUI m_combo;

        [SerializeField]
        private TextMeshProUGUI m_perfect;

        [SerializeField]
        private TextMeshProUGUI m_great;

        [SerializeField]
        private TextMeshProUGUI m_miss;

        [SerializeField]
        private TextMeshProUGUI m_score;

        [SerializeField]
        private TextMeshProUGUI m_newMaxScore;

        [SerializeField]
        private TextMeshProUGUI m_maxScore;

        [Header("Sprites")]
        [SerializeField]
        private Image m_image;

        [SerializeField]
        private Sprite m_win;

        [SerializeField]
        private Sprite m_lose;

        [SerializeField]
        private Image m_sprite;

        [SerializeField]
        private Sprite m_winSprite;

        [SerializeField]
        private Sprite m_loseSprite;

        private void Win()
        {
            UpdateSprites(m_win, m_winSprite);
            UpdateEndInfo();
        }

        private void Lose()
        {
            UpdateSprites(m_lose, m_loseSprite);
            UpdateEndInfo();
        }

        private void UpdateSprites(Sprite background, Sprite Icon)
        {
            m_image.sprite = background;
            m_sprite.sprite = Icon;
        }

        private void UpdateEndInfo()
        {
            m_combo.SetText(Crocodile.BestStreak.ToString());
            m_perfect.SetText(Crocodile.HitCounts[Quality.Perfect].ToString());
            m_great.SetText(Crocodile.HitCounts[Quality.Good].ToString());
            m_miss.SetText(Crocodile.HitCounts[Quality.Bad].ToString());
            m_score.SetText(Crocodile.Score.ToString());
            m_newMaxScore.SetText(Crocodile.Score > LevelController.Instance.SelectedLevelMaxScore ? "New Max Score" : "");
            m_maxScore.SetText(LevelController.Instance.SelectedLevelMaxScore.ToString());
        }

        protected override Dictionary<string, Action> Actions =>
            m_actions ??
            (m_actions = new Dictionary<string, Action>
            {
                { "LevelWin", Win },
                { "LevelLose", Lose }
            });

        private Crocodile Crocodile => Crocodile.Instance;
    }
}