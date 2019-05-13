using DG.Tweening;
using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(CameraGizmo))]
    public class CameraPosition : GenericComponent<CameraPosition, string>
    {
        [SerializeField]
        [Range(0, 179)]
        private float m_fieldOfView = 0f;

        [SerializeField]
        private float m_time = 1f;

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

        private void ActiveChild()
        {
            if(transform.childCount == 0)
            {
                return;
            }

            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(false);
                child.gameObject.SetActive(true);
            }
        }

        public string Name => m_name;

        public override GenericManager<CameraPosition, string> Manager => CameraManager.Instance;

        protected override CameraPosition Component => this;

        private float TweenTime => Time.time <= 0.01f ? 0f : m_time;
    }
}