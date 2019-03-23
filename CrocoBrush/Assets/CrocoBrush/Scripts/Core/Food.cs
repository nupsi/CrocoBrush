using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Controls Food placed on a Tooth.
    /// Use Initialize(Tooth, float) to place the Food on a Tooth.
    /// Use Remove() to remove the Food from its current Tooth.
    /// Use Clear() to mark the parent Tooth as a free Tooth.
    /// </summary>
    public class Food : MonoBehaviour
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Child object that displays the Foods 'age'.
        /// </summary>
        private GameObject m_circle;

        /// <summary>
        /// The parent Tooth for a Food.
        /// </summary>
        private Tooth m_tooth;

        /*
         * Mono Behaviour Functions.
         */

        private void Awake()
        {
            if (transform.childCount > 0)
            {
                m_circle = transform.GetChild(0).gameObject;
            }
            else
            {
                Debug.LogError("There is no child on Food object! (Add a child to represent the time left)");
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            //Reset the Foods quality.
            Quality = Quality.Bad;
            //Reset the time indicator scale.
            m_circle.transform.localScale = Vector3.one * 2;
        }

        private void OnDisable()
        {
            m_tooth = null;
        }

        /*
         * Functions.
         */

        /// <summary>
        /// Initialize the Food for a given Tooth with a given duration.
        /// The Foods lasts a little longer than the given duration.
        /// </summary>
        /// <param name="tooth">Parent Tooth.</param>
        /// <param name="duration">Foods duration.</param>
        public void Initialize(Tooth tooth, float duration)
        {
            //Set the current parent Tooth.
            m_tooth = tooth;
            //Start modifying the Foods quality over time.
            StartCoroutine(Degrade(duration));
            //Start Tween to indicate the Foods lifespan.
            m_circle.transform
                .DOScale(Vector3.one, duration)
                .SetEase(Ease.Linear)
                .OnComplete(
                    () => m_circle.transform
                        .DOScale(Vector3.one, 0.3f)
                        .SetEase(Ease.Linear)
                        .OnComplete(Remove));
        }

        /// <summary>
        /// Modifies the Foods quality over time.
        /// The Foods lasts a little longer than the given duration.
        /// </summary>
        /// <param name="duration">Foods duration.</param>
        private IEnumerator Degrade(float duration)
        {
            yield return new WaitForSeconds(duration * 0.4f);
            Quality = Quality.Avarage;
            yield return new WaitForSeconds(duration * 0.5f);
            Quality = Quality.Good;
            yield return new WaitForSeconds(0.29f);
            Quality = Quality.Bad;
        }

        /// <summary>
        /// Sends a clear request for the current parent Tooth.
        /// </summary>
        public void Clear() => m_tooth?.Clear();

        /// <summary>
        /// Sends a remove request for the current parent Tooth.
        /// </summary>
        private void Remove() => m_tooth?.Remove();

        /*
         * Accessors.
         */

        /// <summary>
        /// Stores the Foods quality.
        /// Use Degrade(float) to change the Foods quality over time.
        /// </summary>
        /// <value>The Foods current quality.</value>
        public Quality Quality { get; private set; }
    }
}