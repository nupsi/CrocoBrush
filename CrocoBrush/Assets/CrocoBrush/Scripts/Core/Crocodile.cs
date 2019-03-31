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
            EventManager.Instance.TriggerEvent("Hit");
            EventManager.Instance.TriggerEvent("UpdateGameUI");
        }

        public void Annoy()
        {
            Anger++;
            EventManager.Instance.TriggerEvent("Miss");
            EventManager.Instance.TriggerEvent("UpdateGameUI");
        }

        public int Score { get; private set; }
        public int Anger { get; private set; }
    }
}