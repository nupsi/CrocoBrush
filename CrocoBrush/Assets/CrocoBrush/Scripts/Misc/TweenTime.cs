using DG.Tweening;
using UnityEngine;

namespace CrocoBrush
{
    public class TweenTime : MonoBehaviour
    {
        private void Update()
        {
            DOTween.ManualUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }
    }
}