using UnityEngine;

namespace CrocoBrush.UI.Menu
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField]
        private LevelData m_data;

        public void Select()
        {
            if(m_data != null)
            {
                Controller.SelectedLevel = m_data;
                EventManager.Instance.TriggerEvent("ChangeLevel");
            }
        }

        public void Play()
        {
            Controller.PlaySelectedLevel();
        }

        private LevelController Controller => LevelController.Instance;
    }
}