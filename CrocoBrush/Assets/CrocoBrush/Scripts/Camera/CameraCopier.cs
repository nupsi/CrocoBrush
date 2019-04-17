using UnityEngine;

namespace CrocoBrush.Editors
{
    [RequireComponent(typeof(Camera))]
    public class CameraCopier : MonoBehaviour
    {
#if UNITY_EDITOR
        private bool m_enabled = false;

        private void OnDrawGizmosSelected()
        {
            if(m_enabled)
            {
                var current = GetComponent<Camera>();
                foreach(var child in GetComponentsInChildren<Camera>())
                {
                    if(child.GetInstanceID() != current.GetInstanceID())
                    {
                        child.useOcclusionCulling = current.useOcclusionCulling;
                        child.orthographicSize = current.orthographicSize;
                        child.nearClipPlane = current.nearClipPlane;
                        child.renderingPath = current.renderingPath;
                        child.targetDisplay = current.targetDisplay;
                        child.farClipPlane = current.farClipPlane;
                        child.orthographic = current.orthographic;
                        child.fieldOfView = current.fieldOfView;
                        child.allowMSAA = current.allowMSAA;
                        child.allowHDR = current.allowHDR;
                        child.rect = current.rect;
                    }
                }
            }
        }

#endif
    }
}