﻿using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

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
        /// Used to visualize the Food's age.
        /// </summary>
        [SerializeField]
        private GameObject m_circle;

        /// <summary>
        /// Displayd when the food is removed when the quality is bad.
        /// </summary>
        [SerializeField]
        private GameObject m_miss;

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

        /// <summary>
        /// Tween sequence to visualize food's lifespan.
        /// </summary>
        private Sequence m_mainTween;

        /// <summary>
        /// Tween sequence to hide the food.
        /// </summary>
        private Sequence m_exitTween;

        /*
         * Mono Behaviour Functions.
         */

        protected void Awake()
        {
            //Get required components.
            m_background = GetComponentInChildren<BackgroundColor>();
            m_collider = GetComponent<BoxCollider>();
        }

        protected void OnEnable()
        {
            //Show required child objects.
            SetAlive(true);
            //Reset the Foods quality.
            Quality = Quality.Bad;
        }

        protected void OnDisable()
        {
            //Make sure the active tween is killed.
            m_mainTween?.Kill();
            m_exitTween?.Kill();
            //Make sure the current parent is cleared.
            ClearParent();
        }

        protected void OnMouseDown()
        {
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                //Send request to remove the pressed Food.
                RemoveFood();
                //Disable collision to prevent clicking after first time.
                m_collider.enabled = false;
            }
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
            //Set parent tooth.
            m_tooth = tooth;
            //Update backgroudn color to match the tooth's direction.
            m_background?.UpdateMaterial(tooth.Direction);
            //Start coroutine to degrade the food's quality.
            StartCoroutine(m_degrade = Degrade(duration));
            //Main tween sequence to visualize the food's lifespan and quality.
            //NOTE: This sequence uses manual update.
            m_mainTween = DOTween.Sequence()
                .OnStart(() => m_circle.transform.localScale = Vector3.one * 2)
                .Append(m_circle.transform.DOScale(Vector3.one, duration).SetEase(Ease.Linear))
                .Append(m_circle.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.Linear))
                .OnComplete(() =>
                {
                    Quality = Quality.Bad;
                    RemoveFood();
                })
                .SetUpdate(UpdateType.Manual)
                .Play();
        }

        /// <summary>
        /// Deactive target food with tweening based on the food quality.
        /// </summary>
        public void Hide()
        {
            ClearParent();
            m_mainTween?.Kill();
            StopCoroutine(m_degrade);
            SetAlive(false, Quality == Quality.Bad);
            m_exitTween = DOTween.Sequence()
                .Append(transform.DOScale(0.5f, 0.2f).SetEase(Ease.InBack))
                .Append(transform.DOScale(1, 0))
                .OnComplete(() => gameObject.SetActive(false))
                .SetUpdate(UpdateType.Manual)
                .Play();
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
            m_tooth?.Remove();
            ClearParent();
        }

        /// <summary>
        /// Active/Deactive collider and time visualization if alive.
        /// Dispaly miss icon if miss = true.
        /// </summary>
        /// <param name="alive">Is the food active.</param>
        /// <param name="miss">Display miss icon.</param>
        private void SetAlive(bool alive, bool miss = false)
        {
            m_circle.SetActive(alive);
            m_miss.SetActive(miss);
            m_collider.enabled = alive;
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
    }
}