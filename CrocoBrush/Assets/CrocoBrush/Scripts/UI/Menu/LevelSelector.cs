using UnityEngine;

namespace CrocoBrush.UI.Menu
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField]
        private LevelData m_data;

        public void Select()
        {
            Controller.SelectedLevel = m_data;
            Controller.PlaySelectedLevel();
        }

        private LevelController Controller => LevelController.Instance;
    }
}