using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    public class Crocodile : MonoBehaviour
    {
        public static Crocodile Instance;

        private SongStats m_stats;

        protected void Awake()
        {
            if(Instance != null)
            {
                Debug.LogError("Multiple Crocodile Instances");
            }
            Instance = this;
            InitializeValues();
        }

        public void StopGame() => Mouth.Instance.Restart();

        public void AddScore(Quality quality)
        {
            ProcessQuality(quality);
            m_stats.AddScore(quality);
            EventManager.Instance.TriggerEvent(quality > 0 ? "Hit" : "Miss", "UpdateGameUI");
        }

        public void Restart()
        {
            Mouth.Instance.Restart();
            InitializeValues();
            EventManager.Instance.TriggerEvent("ResetGame");
        }

        private void InitializeValues()
        {
            var name = LevelController.Instance.SelectedLevel.Name;
            var noteCount = LevelController.Instance.SelectedLevel.Notes.Nodes.Count;
            m_stats = new SongStats(name, noteCount);
            Anger = 0;
            Streak = 0;
        }

        private void ProcessQuality(Quality quality)
        {
            if(quality > 0)
            {
                Hit();
            }
            else
            {
                Miss();
            }
        }

        private void Hit()
        {
            m_stats.CheckStreak(++Streak);
            if(Streak % 10 == 0)
            {
                Anger = Mathf.Clamp(--Anger, 0, int.MaxValue);
            }
        }

        private void Miss()
        {
            Streak = 0;
            Anger++;
        }

        public int Streak { get; private set; }

        public int Anger { get; private set; }

        public int Score => m_stats.Score;

        public int BestStreak
        {
            get => m_stats.BestStreak;
            set => m_stats.BestStreak = value;
        }

        public Dictionary<Quality, int> HitCounts => m_stats.HitCount;
    }
}