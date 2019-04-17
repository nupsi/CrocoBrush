using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(AudioSource))]
    public class LevelController : MonoBehaviour
    {
        public static LevelController Instance;

        [SerializeField]
        private LevelData m_current;

        private AudioSource m_source;

        private SongReader m_noteReader;
        private SongReader m_spaceReader;

        private void Awake()
        {
            if(Instance != null)
            {
                Debug.LogError("Multiple Level Controller Instances!");
                return;
            }
            Instance = this;
            m_source = GetComponent<AudioSource>();
        }

        private void Reset()
        {
            m_source = GetComponent<AudioSource>();
            m_source.playOnAwake = false;
        }

        public void PlaySelectedLevel()
        {
            Crocodile.Instance.Restart();
            EventManager.Instance.TriggerEvent("LevelStart");
            Mouth.Instance.Delay = SelectedLevel.Delay;

            m_source.Stop();
            m_source.clip = SelectedLevel.Audio;
            m_source.time = 0;

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

        public void Pause()
        {
            if(m_source != null)
            {
                m_source.Pause();
            }
        }

        public void UnPause()
        {
            if(m_source != null)
            {
                m_source.UnPause();
            }
        }

        public void Stop()
        {
            Crocodile.Instance.StopGame();
            m_source.Stop();
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