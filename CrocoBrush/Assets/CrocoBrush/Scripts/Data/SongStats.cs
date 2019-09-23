using System;
using System.Collections.Generic;

namespace CrocoBrush
{
    [Serializable]
    public struct SongStats
    {
        public string Name;
        public int NoteCount;
        public int Score;
        public int BestStreak;
        public List<QualityCount> QualityCount;

        public SongStats(string name, int noteCount)
        {
            Name = name;
            NoteCount = noteCount;
            Score = 0;
            BestStreak = 0;
            QualityCount = new List<QualityCount>
            {
                new QualityCount(Quality.Bad),
                new QualityCount(Quality.Good),
                new QualityCount(Quality.Perfect)
            };
        }

        public void AddScore(Quality quality)
        {
            for(int i = 0; i < QualityCount.Count; i++)
            {
                if(QualityCount[i].Quality == quality)
                {
                    QualityCount[i].Count++;
                }
            }
            Score += (int)quality;
        }

        public void CheckStreak(int current)
        {
            BestStreak = Math.Max(BestStreak, current);
        }

        public Dictionary<Quality, int> HitCount
        {
            get
            {
                var dictionary = new Dictionary<Quality, int>();
                foreach(var quality in QualityCount)
                {
                    dictionary.Add(quality.Quality, quality.Count);
                }
                return dictionary;
            }
        }
    }

    [Serializable]
    public class QualityCount
    {
        public Quality Quality;
        public int Count;

        public QualityCount(Quality quality)
        {
            Quality = quality;
            Count = 0;
        }
    }
}