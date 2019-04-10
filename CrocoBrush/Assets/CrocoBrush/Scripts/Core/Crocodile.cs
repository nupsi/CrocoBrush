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
        }

        public void AddScore(int score)
        {
            Score += score;
            EventManager.Instance.TriggerEvent("UpdateGameUI");
        }

        public void Annoy()
        {
            Anger++;
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
            this.Score = 0;
            this.Anger = 0;
            EventManager.Instance.TriggerEvent("ResetGame");
        }

        public int Score { get; private set; }
        public int Anger { get; private set; }
    }
}