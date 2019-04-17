using DG.Tweening;
using UnityEngine;

namespace CrocoBrush.Tweening
{
    public class TweenTime : MonoBehaviour
    {
        private void OnEnable()
        {
            LevelController.Instance?.UnPause();
        }

        private void OnDisable()
        {
            LevelController.Instance?.Pause();
        }

        private void Update()
        {
            DOTween.ManualUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }
    }
}