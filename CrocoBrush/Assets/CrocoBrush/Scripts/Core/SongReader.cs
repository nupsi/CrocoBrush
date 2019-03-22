using System.Collections;
using UnityEngine;

namespace CrocoBrush
{
    public class SongReader : MonoBehaviour
    {
        [SerializeField]
        private Mouth m_mouth;

        [SerializeField]
        private SongNotes m_song;

        private int current = 0;

        private void Awake()
        {
            StartSong();
        }

        public void StartSong()
        {
            StartCoroutine(PlayNext());
        }

        private IEnumerator PlayNext()
        {
            while(current < m_song.Nodes.Count)
            {
                yield return new WaitForSeconds(Time);
                m_mouth.Up();
            }
        }

        private float Time
        {
            get
            {
                var time = 0f;
                if(current == 0)
                {
                    time = m_song.Nodes[current].Time;
                }
                else
                {
                    if(current < m_song.Nodes.Count)
                    {
                        time = m_song.Nodes[current].Time - m_song.Nodes[current - 1].Time;
                    }
                }
                current++;
                return time;
            }
        }
    }
}