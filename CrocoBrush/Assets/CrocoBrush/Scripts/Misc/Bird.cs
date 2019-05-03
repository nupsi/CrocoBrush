using DG.Tweening;
using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Animation controller for the Bird.
    /// Can be used to move the Bird or play animations.
    /// </summary>
    public class Bird : MonoBehaviour
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Current Bird Instance.
        /// </summary>
        public static Bird Instance;

        /// <summary>
        /// The bird's Animator component.
        /// Used when triggering differend animations.
        /// </summary>
        private Animator m_aimator;

        /// <summary>
        /// Bird data containing moving positions.
        /// </summary>
        private BirdData m_data;

        /// <summary>
        /// Current Tweening sequence.
        /// </summary>
        private Sequence m_sequence;

        /*
         * Mono Behaviour Functions.
         */

        private void Awake()
        {
            if(Instance != null)
            {
                Debug.LogError("Bird Instance already exists!");
                return;
            }

            Instance = this;
            m_aimator = GetComponentInChildren<Animator>();
        }

        /*
         * Functions.
         */

        /// <summary>
        /// Plays the bird's eat animation.
        /// </summary>
        public void PlayEatAnimation() => m_aimator.SetTrigger("Eat");

        /// <summary>
        /// Start the bird's wing block animation.
        /// </summary>
        public void PlayBlockAnimation() => m_aimator.SetTrigger("Block");

        /// <summary>
        /// End the bird's wing block animation.
        /// </summary>
        public void PlayUnblocAnimation() => m_aimator.SetTrigger("Unblock");

        /// <summary>
        /// Start the bird's flying animation.
        /// </summary>
        public void StartTravel() => m_aimator.SetTrigger("StartTravel");

        /// <summary>
        /// Stop the bird's flying animation.
        /// </summary>
        public void StopTravel() => m_aimator.SetTrigger("EndTravel");

        /// <summary>
        /// Set bird data containing target positions for movement.
        /// </summary>
        /// <param name="data">Bird data containing target positions.</param>
        public void SetData(BirdData data) => m_data = data;

        /// <summary>
        /// Moves the Bird to a given position.
        /// </summary>
        /// <param name="target">Target position.</param>
        public void MoveToPoint(Transform target)
        {
            if(NeedToMove(target.position))
            {
                DOTween.Kill(transform);
                m_sequence.Kill();
                var time = 0.75f;
                var rotation = GetTargetRotation(target.position);
                m_sequence = DOTween.Sequence()
                    .Append(transform.DORotate(rotation, time * 0.25f).OnComplete(StartTravel))
                    .Append(transform.DOMove(target.transform.position, time * 0.5f).OnComplete(StopTravel))
                    .Append(transform.DORotate(target.transform.rotation.eulerAngles, time * 0.25f))
                    .Play();
            }
        }

        /// <summary>
        /// Moves the bird to the given position and plays the eat animation.
        /// </summary>
        /// <param name="target">Target position.</param>
        public void EatAt(Transform target)
        {
            if(!NeedToMove(target.position))
            {
                PlayEatAnimation();
                return;
            }

            DOTween.Kill(transform);
            m_sequence.Kill();
            var time = 0.4f;
            var rotation = GetTargetRotation(target.position);
            m_sequence = DOTween.Sequence()
                .OnStart(StartTravel)
                .Append(transform.DOMove(target.transform.position, time).OnComplete(StopTravel))
                .Join(transform.DORotate(rotation, time * 0.25f))
                .Append(transform.DORotate(target.transform.rotation.eulerAngles, time * 0.25f))
                .OnComplete(PlayEatAnimation)
                .Play();
        }

        /// <summary>
        /// Get euler angles to look towards the target.
        /// </summary>
        /// <returns>Rotation to face the target position.</returns>
        /// <param name="target">Target position.</param>
        private Vector3 GetTargetRotation(Vector3 target)
        {
            var y = Mathf.Abs(transform.position.y - target.y);
            var distance = Vector3.Distance(transform.position, target);
            if(distance * 0.9f < y)
            {
                target.y = transform.position.y;
            }
            return Quaternion.LookRotation(transform.position - target).eulerAngles;
        }

        /// <summary>
        /// Checks if the distance between current and target positions is long enough 
        /// for requiring movement.
        /// </summary>
        /// <returns>Is movement needed to reach the target position.</returns>
        /// <param name="target">Target position.</param>
        private bool NeedToMove(Vector3 target) => Vector3.Distance(transform.position, target) > 0.01f;

        /// <summary>
        /// Turns given direction into a corresponding transform.
        /// </summary>
        /// <param name="direction">Direction for transform.</param>
        /// <returns>Transform for direction.</returns>
        public Transform GetPosition(Direction direction) => m_data?.GetDirection(direction) ?? transform;
    }
}