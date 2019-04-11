using System;
using System.Collections;
using UnityEngine;

namespace CrocoBrush
{
    [Obsolete("This class should not be required. See SongReader for the lates song/note delay.", false)]
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