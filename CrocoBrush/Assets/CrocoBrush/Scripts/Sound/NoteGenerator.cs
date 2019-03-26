using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush.Sound
{
    public class NoteGenerator : Analyzer
    {
        [SerializeField]
        private SongNotes m_songNotes;

        private float m_startTime;

        protected override void RequestInput()
        {
            m_songNotes.Nodes.Add(new SongNode(Direction.None, m_source.time));
        }

        public void StartRecord()
        {
            m_startTime = Time.time;
            m_songNotes.Nodes = new List<SongNode>();
            m_source.Play();
        }
    }
}