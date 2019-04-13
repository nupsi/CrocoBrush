using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    public class Crocodile : MonoBehaviour
    {
        public static Crocodile Instance;

        private void Awake()
        {
            if(Instance != null)
            {
                Debug.LogError("Multiple Crocodile Instances");
            }
            Instance = this;
            InitializeValues();
        }

        public void AddScore(Quality quality)
        {
            HitCounts[quality]++;
            if(quality <= 0)
            {
                Annoy();
                EventManager.Instance.TriggerEvent("Miss");
            }
            else
            {
                ProcessQuality(quality);
                EventManager.Instance.TriggerEvent("Hit");
            }

            EventManager.Instance.TriggerEvent("UpdateGameUI");
        }

        public void CalmDown()
        {
            if(Anger > 0)
            {
                Anger--;
                EventManager.Instance.TriggerEvent("UpdateGameUI");
            }
        }

        public void Restart()
        {
            Mouth.Instance.Restart();
            InitializeValues();
            EventManager.Instance.TriggerEvent("ResetGame");
        }

        private void Annoy()
        {
            Streak = 0;
            Anger++;
        }

        private void ProcessQuality(Quality quality)
        {
            Score += (int)quality;
            Streak++;
            if(Streak > BestStreak)
            {
                BestStreak = Streak;
            }
        }

        private void InitializeValues()
        {
            Score = 0;
            Anger = 0;
            Streak = 0;
            BestStreak = 0;
            HitCounts = new Dictionary<Quality, int>()
            {
                { Quality.Bad, 0 },
                { Quality.Good, 0  },
                { Quality.Perfect, 0 }
            };
        }

        public int Score { get; private set; }
        public int Streak { get; private set; }
        public int BestStreak { get; private set; }
        public int Anger { get; private set; }
        public Dictionary<Quality, int> HitCounts { get; private set; }
    }
}