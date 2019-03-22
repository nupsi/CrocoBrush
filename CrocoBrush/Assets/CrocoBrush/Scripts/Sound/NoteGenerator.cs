using CrocoBrush;
using UnityEngine;

namespace Test.Sound
{
    public class NoteGenerator : Analyzer
    {
        [SerializeField]
        private SongNotes m_songNotes;

        private float m_startTime;

        protected override void RequestInput()
        {
            m_songNotes.Nodes.Add(new SongNode(Direction.None, Time.time - m_startTime));
        }

        public void StartRecord()
        {
            m_startTime = Time.time;
            m_source.Play();
        }
    }
}