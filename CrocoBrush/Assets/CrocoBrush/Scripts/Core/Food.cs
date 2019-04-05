using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Controls Food placed on a Tooth.
    /// Use Initialize(Tooth, float) to place the Food on a Tooth.
    /// </summary>
    [RequireComponent(typeof(BoxCollider))]
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
        /// Controller for changing background color to visualize current direction.
        /// </summary>
        private BackgroundColor m_background;

        /// <summary>
        /// Current degrade loop.
        /// Used to start and stop the degrading process.
        /// </summary>
        private IEnumerator m_degrade;

        /// <summary>
        /// Current parent Tooth.
        /// </summary>
        private Tooth m_tooth;

        /// <summary>
        /// Box collider for detecting click.
        /// Enabled and disabled to prevent multiple clicks.
        /// </summary>
        private BoxCollider m_collider;

        /*
         * Mono Behaviour Functions.
         */

        private void Awake()
        {
            if(transform.childCount > 0)
            {
                m_circle = transform.GetChild(0).gameObject;
                m_background = GetComponentInChildren<BackgroundColor>();
                m_collider = GetComponent<BoxCollider>();
            }
            else
            {
                Debug.LogError("There is no child on Food object! (Add a child to represent the time left)");
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            m_circle.SetActive(true);
            //Reset the Foods quality.
            Quality = Quality.Bad;
            //Reset the time indicator scale.
            m_circle.transform.localScale = Vector3.one * 2;
            //Enable collision.
            m_collider.enabled = true;
        }

        private void OnDisable()
        {
            //Make sure the active tween is killed.
            DOTween.Kill(m_circle.transform);
            //Make sure the current parent is cleared.
            ClearParent();
        }

        private void OnMouseDown()
        {
            //Send request to remove the pressed Food.
            RemoveFood();
            //Disable collision to prevent clicking after first time.
            m_collider.enabled = false;
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
            m_tooth = tooth;
            m_background?.UpdateMaterial(tooth.Direction);
            //Start modifying the Foods quality over time.
            m_degrade = Degrade(duration);
            StartCoroutine(m_degrade);
            //Start Tween to indicate the Foods lifespan.
            m_circle.transform
                .DOScale(Vector3.one, duration)
                .SetEase(Ease.Linear)
                .OnComplete(
                    () => m_circle.transform
                        .DOScale(Vector3.one, 0.3f)
                        .SetEase(Ease.Linear)
                        .OnComplete(() =>
                        {
                            Quality = Quality.Bad;
                            RemoveFood();
                        })
                );
        }

        /// <summary>
        /// Deactive target food with tweening based on the food quality.
        /// </summary>
        public void Hide()
        {
            ClearParent();
            DOTween.Kill(m_circle.transform);
            var size = TargetSize;
            StopCoroutine(m_degrade);
            m_circle.SetActive(false);
            transform.DOScale(size, 0.3f * size)
                .SetEase(size < 1 ? Ease.InBack : Ease.OutBack)
                .OnComplete(() =>
                {
                    transform.DOScale(1, 0);
                    gameObject.SetActive(false);
                });
        }

        /// <summary>
        /// Modifies the Foods quality over time.
        /// The Foods lasts a little longer than the given duration.
        /// </summary>
        /// <param name="duration">Foods duration.</param>
        private IEnumerator Degrade(float duration)
        {
            yield return new WaitForSeconds(duration * 0.5f);
            Quality = Quality.Good;
            yield return new WaitForSeconds(duration * 0.5f);
            Quality = Quality.Perfect;
        }

        /// <summary>
        /// Send a remove request to the current parent Tooth.
        /// </summary>
        private void RemoveFood()
        {
            if(m_tooth != null)
            {
                m_tooth.Remove();
                ClearParent();
            }
            else
            {
                Debug.LogError("Trying to remove Food that no longer exists!");
            }
        }

        /// <summary>
        /// Clear the current parent Tooth.
        /// </summary>
        private void ClearParent() => m_tooth = null;

        /*
         * Accessors.
         */

        /// <summary>
        /// Stores the Foods quality.
        /// Use Degrade(float) to change the Foods quality over time.
        /// </summary>
        /// <value>The Foods current quality.</value>
        public Quality Quality { get; private set; }

        /// <summary>
        /// Target size for end tween based on the quality.
        /// </summary>
        /// <value>The target size.</value>
        private float TargetSize
        {
            get
            {
                switch(Quality)
                {
                    case Quality.Bad: return 0.5f;
                    case Quality.Good: return 1.25f;
                    case Quality.Perfect: return 1.5f;
                    default: return 1f;
                }
            }
        }
    }
}