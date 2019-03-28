using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(SongReader))]
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private LevelData m_current;

        private SongReader m_reader;

        private void Awake()
        {
            m_reader = GetComponent<SongReader>();
        }

        private void Start()
        {
            PlaySelectedLevel();
        }

        public void PlaySelectedLevel()
        {
            Mouth.Instance.Delay = SelectedLevel.Delay;
            m_reader.SetSong(SelectedLevel.Audio, SelectedLevel.Notes);
            m_reader.StartSong();
        }

        public LevelData SelectedLevel
        {
            get => m_current;
            set => m_current = value;
        }
    }
}