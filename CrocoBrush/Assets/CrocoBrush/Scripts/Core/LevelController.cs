using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(AudioSource))]
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private LevelData m_current;

        private AudioSource m_source;

        private SongReader m_noteReader;
        private SongReader m_spaceReader;

        private void Awake()
        {
            m_source = GetComponent<AudioSource>();
        }

        private void Start()
        {
            PlaySelectedLevel();
        }

        private void Reset()
        {
            m_source = GetComponent<AudioSource>();
            m_source.playOnAwake = false;
        }

        public void PlaySelectedLevel()
        {
            Mouth.Instance.Delay = SelectedLevel.Delay;
            m_source.clip = SelectedLevel.Audio;

            if(Mouth.Instance != null)
            {
                m_noteReader = new SongReader(Mouth, m_source, SelectedLevel.Notes);
                m_noteReader.StartSong();
            }

            if(Spacebar.Instance != null)
            {
                m_spaceReader = new SongReader(Spacebar, m_source, SelectedLevel.SpaceNotes);
                m_spaceReader.StartSong();
            }
        }

        public LevelData SelectedLevel
        {
            get => m_current;
            set => m_current = value;
        }

        private Mouth Mouth => Mouth.Instance;
        private Spacebar Spacebar => Spacebar.Instance;
    }
}