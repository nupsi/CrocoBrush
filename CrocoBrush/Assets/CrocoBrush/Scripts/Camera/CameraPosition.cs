using DG.Tweening;
using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(CameraGizmo))]
    public class CameraPosition : MonoBehaviour
    {
        [SerializeField]
        private string m_name = "None";

        [SerializeField]
        [Range(0, 179)]
        private float m_fieldOfView = 0f;

        [SerializeField]
        private float m_time = 1f;

        public void OnEnable()
        {
            CameraManager.Instance.RegisterComponent(this);
        }

        public void OnDisable()
        {
            CameraManager.Instance.RemoveComponent(this);
        }

        public void SetCamera(Camera camera)
        {
            DOTween.Kill(camera);
            DOTween.Kill(camera.transform);

            if(m_fieldOfView > 0)
            {
                camera.DOFieldOfView(m_fieldOfView, TweenTime);
            }

            camera.transform
                .DOMove(transform.position, TweenTime)
                .SetEase(Ease.Linear);

            camera.transform
                .DORotate(transform.rotation.eulerAngles, TweenTime)
                .SetEase(Ease.Linear);
        }

        public string Name => m_name;

        private float TweenTime => Time.time == 0 ? 0f : m_time;
    }
}