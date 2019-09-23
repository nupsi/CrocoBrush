using DG.Tweening;
using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Reads Song Notes scriptable object data to create
    /// </summary>
    public class SongReader
    {
        /*
         * Variables.
         */

        private const string DestroyEvent = "LevelEnd";

        /// <summary>
        /// Current song notes to play.
        /// </summary>
        private readonly SongNotes m_song;

        /// <summary>
        /// Current audio source for playing the music.
        /// This is played with delay with the PlaySong() function.
        /// </summary>
        private readonly AudioSource m_source;

        /// <summary>
        /// The target object for creating objects from the current notes.
        /// </summary>
        private readonly ICreator m_creator;

        /// <summary>
        /// Song playing sequence.
        /// Used to delay the background song from the note generation.
        /// </summary>
        private Sequence m_songSequence;

        /// <summary>
        /// Note playing sequence.
        /// Used to spawn the notes.
        /// </summary>
        private Sequence m_noteSequence;

        /// <summary>
        /// Current Note index.
        /// </summary>
        private int m_current;

        /*
         * Functions.
         */

        public SongReader(ICreator creator, AudioSource source, SongNotes notes)
        {
            EventManager.Instance.StartListening(DestroyEvent, Destroy);
            m_source = source;
            m_creator = creator;
            m_song = notes;
        }

        /// <summary>
        /// Starts the song by starting the process of spawning Food and playing audio with delay.
        /// </summary>
        public void StartSong()
        {
            PlaySong();
            PlayNote();
        }

        /// <summary>
        /// Start playing the audio with a delay.
        /// The delay is the time it takes for the circle around the Food to close in.
        /// </summary>
        private void PlaySong()
        {
            m_songSequence = DOTween.Sequence()
                .PrependInterval(Mouth.Instance.Delay)
                .OnComplete(() => m_source.Play())
                .SetUpdate(UpdateType.Manual)
                .Play();
        }

        /// <summary>
        /// Play the current note with the note delay.
        /// </summary>
        private void PlayNote()
        {
            //Continue to play after the game is lost.
            m_noteSequence = DOTween.Sequence()
                .PrependInterval(m_song.Nodes[m_current].Delay)
                .OnComplete(() =>
                {
                    m_creator.Create(m_song.Nodes[m_current].Direction);
                    if(Crocodile.Instance.gameObject.activeInHierarchy)
                    {
                        m_current++;
                        if(m_current < m_song.Nodes.Count)
                        {
                            PlayNote();
                        }
                    }
                })
                .SetUpdate(UpdateType.Manual)
                .Play();
        }

        /// <summary>
        /// Remove the current song reader from the event manager and kill the current tweens.
        /// </summary>
        private void Destroy()
        {
            EventManager.Instance.StopListening(DestroyEvent, Destroy);
            m_songSequence.Kill();
            m_noteSequence.Kill();
        }
    }
}