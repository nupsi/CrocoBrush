using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CrocoBrush.UI.Menu
{
    public class ShowLevelInfo : RegisteredBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_name;

        [SerializeField]
        private TextMeshProUGUI m_difficulty;

        [SerializeField]
        private TextMeshProUGUI m_duration;

        private void UpdateLevelInfo()
        {
            m_name.SetText(Name);
            m_difficulty.SetText(Difficulty);
            m_duration.SetText(Duration);
        }

        private string Duration
        {
            get
            {
                var duration = Mathf.Round(LevelController.Instance.SelectedLevel.Audio.length);
                var minutes = Mathf.Floor(duration / 60);
                var seconds = duration % 60;
                return $"Duration {minutes}:{seconds}";
            }
        }

        private string Name => LevelController.Instance.SelectedLevel.Name;

        private string Difficulty => LevelController.Instance.SelectedLevel.Difficulty;

        protected override Dictionary<string, Action> Actions =>
            m_actions ??
            (m_actions = new Dictionary<string, Action>
            {
                { "ChangeLevel", UpdateLevelInfo }
            });
    }
}