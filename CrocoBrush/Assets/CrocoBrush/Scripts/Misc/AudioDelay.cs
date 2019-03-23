using System.Collections;
using UnityEngine;
namespace CrocoBrush
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioDelay : MonoBehaviour
    {
        [SerializeField]
        private float m_delay = 1f;

        private void Awake()
        {
            StartCoroutine(Delay());
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(m_delay);
            GetComponent<AudioSource>().Play();
        }
    }
}