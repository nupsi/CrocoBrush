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
            GUIController.Instance.UpdateComponents();
        }

        public void Annoy()
        {
            Anger++;
            GUIController.Instance.UpdateComponents();
        }

        public int Score { get; private set; }
        public int Anger { get; private set; }
    }
}