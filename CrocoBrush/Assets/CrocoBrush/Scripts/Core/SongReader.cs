using System;
using System.Collections;
using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Reads Song Notes scriptable object data to create
    /// Food in Mouth in sync with the music.
    /// </summary>
    public class SongReader
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Delay between checking the song and next note in seconds.
        /// </summary>
        private readonly WaitForSeconds m_readDelay;

        /// <summary>
        /// Current song notes to play.
        /// </summary>
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
        
        /// <summary>
        /// The target object for creating objects from the current notes.
        /// </summary>
        private ICreator m_creator;

        /*
         * Functions.
         */

        public SongReader(ICreator creator, AudioSource source, SongNotes notes)
        {
            m_source = source;
            m_creator = creator;
            m_song = notes;
            m_readDelay = new WaitForSeconds(0.0001f);
        }

        /// <summary>
        /// Starts the song by starting the process of spawning Food and playing audio with delay.
        /// </summary>
        public void StartSong()
        {
            //Start the Food spwaning loop.
            Mouth.Instance.StartCoroutine(PlayNext());
            //Start the audio with delay.
            Mouth.Instance.StartCoroutine(PlaySong());
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
                    if(m_song.Nodes[m_current].Delay < Delay)
                    {
                        //Wait for the delay time and spawn note.
                        yield return new WaitForSeconds(NoteDelay);
                        m_creator.Create(m_song.Nodes[m_current].Direction);
                    }
                }
                else
                {
                    //Wait for the Note time match with audio sources time.
                    if(CurrentTime >= m_song.Nodes[m_current].Time)
                    {
                        m_creator.Create(m_song.Nodes[m_current].Direction);
                        m_current++;
                    }
                }
                yield return m_readDelay;
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

        /// <summary>
        /// The delay between creating notes and the correct time hitting them.
        /// </summary>
        /// <value>The current delay.</value>
        private float Delay => Mouth.Instance.Delay;
    }
}