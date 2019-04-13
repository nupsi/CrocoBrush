using DG.Tweening;
using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(CameraGizmo))]
    public class CameraPosition : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Positions name.")]
        private string m_name = "None";

        [SerializeField]
        [Range(0, 179)]
        [Tooltip("Target Field of View. If 0 tweening disabled")]
        private float m_fieldOfView = 0f;

        [SerializeField]
        [Tooltip("Tweening Time.")]
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
                camera.DOFieldOfView(m_fieldOfView, m_time);
            }

            camera.transform
                .DOMove(transform.position, m_time)
                .SetEase(Ease.Linear);

            camera.transform
                .DORotate(transform.rotation.eulerAngles, m_time)
                .SetEase(Ease.Linear);
        }

        public string Name => m_name;
    }
}