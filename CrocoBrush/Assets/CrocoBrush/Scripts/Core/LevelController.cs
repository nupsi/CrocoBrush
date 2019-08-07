using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Singleton controller for selected level data.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class LevelController : MonoBehaviour
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Current Level Controller singleton.
        /// </summary>
        public static LevelController Instance;

        /// <summary>
        /// Current playthrough game save data.
        /// </summary>
        [SerializeField]
        private GameSave m_save;

        /// <summary>
        /// Audio source for playing level audio.
        /// </summary>
        private AudioSource m_source;

        /// <summary>
        /// Note reader for generating food notes.
        /// </summary>
        private SongReader m_noteReader;

        /// <summary>
        /// Note reader for generating spacebar notes.
        /// </summary>
        private SongReader m_spaceReader;

        /*
         * Mono Behaviour Functions.
         */

        protected void Awake()
        {
            if(Instance != null)
            {
                Debug.LogError("Multiple Level Controller Instances!");
                return;
            }
            Instance = this;
        }

        protected void Reset()
        {
            AudioSource.playOnAwake = false;
        }

        /*
         * Functions.
         */

        /// <summary>
        /// Start playing the selected level from the start.
        /// </summary>
        public void PlaySelectedLevel()
        {
            EventManager.Instance.TriggerEvent("LevelStart");
            Mouth.Instance.Delay = SelectedLevel.Delay;

            AudioSource.Stop();
            AudioSource.clip = SelectedLevel.Audio;
            AudioSource.time = 0;

            if(Mouth != null)
            {
                m_noteReader = new SongReader(Mouth, AudioSource, SelectedLevel.Notes);
                m_noteReader.StartSong();
            }

            if(Spacebar != null)
            {
                m_spaceReader = new SongReader(Spacebar, AudioSource, SelectedLevel.SpaceNotes);
                m_spaceReader.StartSong();
            }
        }

        /// <summary>
        /// Pause the current song.
        /// </summary>
        public void Pause() => AudioSource?.Pause();

        /// <summary>
        /// Continue the current song.
        /// </summary>
        public void UnPause() => AudioSource?.UnPause();

        /// <summary>
        /// Stop the current song.
        /// </summary>
        public void Stop()
        {
            Crocodile.Instance.StopGame();
            AudioSource.Stop();
        }

        /*
         * Accessors.
         */

        /// <summary>
        /// Selected level data.
        /// </summary>
        public LevelData SelectedLevel { get; set; }

        /// <summary>
        ///  Current playthrough game save data.
        ///  Created on <see cref="PlaySelectedLevel"/>.
        /// </summary>
        public GameSave Save => m_save;

        /// <summary>
        /// Current Mouth Instance.
        /// </summary>
        private Mouth Mouth => Mouth.Instance;

        /// <summary>
        /// Current Spacebar Instance.
        /// </summary>
        private Spacebar Spacebar => Spacebar.Instance;

        /// <summary>
        /// Audio source for playing level audio.
        /// </summary>
        public AudioSource AudioSource => m_source ?? (m_source = GetComponent<AudioSource>());
    }
}