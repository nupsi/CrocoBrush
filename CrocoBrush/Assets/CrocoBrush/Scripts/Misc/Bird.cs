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
        /// Animation trigger for eating animation.
        /// </summary>
        private readonly string Eat = "Eat";

        /// <summary>
        /// Animation trigger for blocking nose animation.
        /// </summary>
        private readonly string Block = "Block";

        /// <summary>
        /// Animation trigger for unblocking nose animation.
        /// </summary>
        private readonly string Unblock = "Unblock";

        /// <summary>
        /// The bird's Animator component.
        /// Used when triggering differend animations.
        /// <see cref="Eat"/>
        /// <see cref="Block"/>
        /// </summary>
        private Animator m_aimator;

        /// <summary>
        /// Bird data containing moving positions.
        /// </summary>
        private BirdData m_data;

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
        public void PlayEatAnimation() => m_aimator.SetTrigger(Eat);

        /// <summary>
        /// Start the bird's wing block animation.
        /// </summary>
        public void PlayBlockAnimation() => m_aimator.SetTrigger(Block);

        /// <summary>
        /// End the bird's wing block animation.
        /// </summary>
        public void PlayUnblocAnimation() => m_aimator.SetTrigger(Unblock);

        /// <summary>
        /// Set bird data containing target positions for movement.
        /// </summary>
        /// <param name="data">Bird data containing target positions.</param>
        public void SetData(BirdData data)
        {
            m_data = data;
        }

        /// <summary>
        /// Moves the Bird to a given position.
        /// </summary>
        /// <param name="target">Target position.</param>
        /// <param name="join">Is the rotation tween join operation.</param>
        /// <param name="eat">Does the bird play the eat animation after the tween.</param>
        public void MoveToPoint(Transform target, bool join = true, bool eat = true)
        {
            DOTween.Kill(transform.position);
            var time = (transform.position == target.position) ? 0 : 0.5f;
            var sequence = DOTween.Sequence().Append(transform.DOMove(target.transform.position, time));

            if(join)
            {
                sequence.Join(transform.DORotate(target.transform.rotation.eulerAngles, time));
            }
            else
            {
                sequence.Append(transform.DORotate(target.transform.rotation.eulerAngles, time));
            }

            if(eat)
            {
                sequence.OnComplete(() => PlayEatAnimation());
            }

            sequence.Play();
        }

        /// <summary>
        /// Turns given direction into a corresponding transform.
        /// </summary>
        /// <param name="direction">Direction for transform.</param>
        /// <returns>Transform for direction.</returns>
        public Transform GetPosition(Direction direction)
        {
            return m_data?.GetDirection(direction) ?? transform;
        }
    }
}