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
            m_image.sprite = m_win;
            m_sprite.sprite = m_winSprite;
            UpdateEndInfo();
        }

        private void Lose()
        {
            m_image.sprite = m_lose;
            m_sprite.sprite = m_loseSprite;
            UpdateEndInfo();
        }

        private void UpdateEndInfo()
        {
            m_combo.SetText(Crocodile.BestStreak.ToString());
            m_perfect.SetText(Crocodile.HitCounts[Quality.Perfect].ToString());
            m_great.SetText(Crocodile.HitCounts[Quality.Good].ToString());
            m_miss.SetText(Crocodile.HitCounts[Quality.Bad].ToString());
            m_score.SetText(Crocodile.Score.ToString());
            Debug.Log(
                LevelController.Instance.Save.GetMaxScore(
                    LevelController.Instance.SelectedLevel.Name
                ));
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