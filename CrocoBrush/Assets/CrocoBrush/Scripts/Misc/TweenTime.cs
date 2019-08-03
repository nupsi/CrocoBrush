using DG.Tweening;
using UnityEngine;

namespace CrocoBrush.Tweening
{
    public class TweenTime : MonoBehaviour
    {
        protected void OnEnable()
        {
            LevelController.Instance?.UnPause();
        }

        protected void OnDisable()
        {
            LevelController.Instance?.Pause();
        }

        protected void Update()
        {
            DOTween.ManualUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }
    }
}