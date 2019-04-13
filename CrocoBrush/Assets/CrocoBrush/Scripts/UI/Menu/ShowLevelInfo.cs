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

        private void UpdateLevelInfo()
        {
            m_name.SetText(LevelController.Instance.SelectedLevel.Name);
            m_difficulty.SetText(LevelController.Instance.SelectedLevel.Difficulty);
        }

        protected override Dictionary<string, Action> Actions =>
            m_actions ??
            (m_actions = new Dictionary<string, Action>
            {
                { "ChangeLevel", UpdateLevelInfo }
            });
    }
}