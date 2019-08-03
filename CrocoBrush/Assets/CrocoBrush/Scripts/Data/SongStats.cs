using System;
using System.Collections.Generic;

namespace CrocoBrush
{
    [Serializable]
    public struct SongStats
    {
        public int NoteCount;
        public int Score;
        public int BestStreak;
        public Dictionary<Quality, int> HitCount;

        public SongStats(int noteCount)
        {
            NoteCount = noteCount;
            Score = 0;
            BestStreak = 0;
            HitCount = new Dictionary<Quality, int>()
            {
                { Quality.Bad, 0 },
                { Quality.Good, 0  },
                { Quality.Perfect, 0 }
            };
        }

        public void AddScore(Quality quality)
        {
            HitCount[quality]++;
            Score += (int)quality;
        }

        public void CheckStreak(int current)
        {
            BestStreak = Math.Max(BestStreak, current);
        }
    }
}