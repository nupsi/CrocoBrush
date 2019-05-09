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

        private void Awake()
        {
            Deactive();
        }

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

            ActiveChild();

            DOTween.Sequence()
                .Append(camera.transform.DOMove(transform.position, TweenTime))
                .Join(camera.transform.DORotate(transform.rotation.eulerAngles, TweenTime))
                .Play();
        }

        public void Deactive()
        {
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        private void ActiveChild()
        {
            if(transform.childCount == 0)
            {
                return;
            }

            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }

        public string Name => m_name;

        private float TweenTime => Time.time == 0 ? 0f : m_time;
    }
}