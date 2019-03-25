using System.Collections;
using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Reads Song Notes scriptable object data to create
    /// Food in Mouth in sync with the music.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SongReader : MonoBehaviour
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Current song notes to play.
        /// </summary>
        [SerializeField]
        private SongNotes m_song;

        /// <summary>
        /// Current audio source for playing the music.
        /// This is played with delay with the PlaySong() function.
        /// </summary>
        private AudioSource m_source;

        /// <summary>
        /// Current Note index.
        /// </summary>
        private int m_current;

        /*
         * Mono Behaviour Functions.
         */

        private void Awake()
        {
            //Cache audio source component.
            m_source = GetComponent<AudioSource>();
        }

        private void Start()
        {
            //Start spawning food and play the audio with delay.
            StartSong();
        }

        private void Reset()
        {
            //Automatically turn off the play on awake, when this script is attached to a game object.
            m_source = GetComponent<AudioSource>();
            m_source.playOnAwake = false;
        }

        /*
         * Functions.
         */

        /// <summary>
        /// Starts the song by starting the process of spawning Food and playing audio with delay.
        /// </summary>
        public void StartSong()
        {
            //Start the Food spwaning loop.
            StartCoroutine(PlayNext());
            //Start the audio with delay.
            StartCoroutine(PlaySong());
        }

        /// <summary>
        /// Loop to go through the Song Notes.
        /// If the audio source time is smaller than the delay,
        /// we use the Note delay for spawning Food.
        /// If the audio source time is greater than the delay,
        /// we use audio sources time to spwan Food based on the Note time.
        /// </summary>
        private IEnumerator PlayNext()
        {
            //Loop until the current index is smaller than the note count.
            while(m_current < m_song.Nodes.Count)
            {
                //Decide between spawing with delay and time.
                if(m_source.time < Delay)
                {
                    //Wait for the delay time and spawn note.
                    yield return new WaitForSeconds(NoteDelay);
                    Mouth.Create(m_song.Nodes[m_current].Direction);
                }
                else
                {
                    //Wait for the Note time match with audio sources time.
                    if(CurrentTime >= m_song.Nodes[m_current].Time)
                    {
                        Mouth.Create(m_song.Nodes[m_current].Direction);
                        m_current++;
                    }
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        /// <summary>
        /// Start playing audio with a delay.
        /// The delay is the time it takes for the circle around the Food to close in.
        /// </summary>
        private IEnumerator PlaySong()
        {
            yield return new WaitForSeconds(Delay);
            m_source.Play();
        }

        /*
         * Accessors.
         */

        /// <summary>
        /// Shorter reference to the current Mouth.Instance.
        /// </summary>
        /// <value>The current Mouth Instance.</value>
        private Mouth Mouth => Mouth.Instance;

        /// <summary>
        /// Current delay between spawning the Food and playing audio.
        /// </summary>
        /// <value>The delay between spawning and playin audio.</value>
        private float Delay => Mouth.Delay;

        /// <summary>
        /// Returns the current audio source time with the delay.
        /// Used to match the spawning of the Food with the audio.
        /// </summary>
        /// <value>The currnt audio souce time with the delay.</value>
        private float CurrentTime => (m_source.time + Delay);

        /// <summary>
        /// Returns the delay time for current Note.
        /// Automatically increments the current index.
        /// </summary>
        /// <value>The delay for the current Note.</value>
        private float NoteDelay
        {
            get
            {
                var time = m_song.Nodes[m_current].Delay;
                m_current++;
                return time;
            }
        }
    }
}