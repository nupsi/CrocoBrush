using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(Animator))]
    public class Bird : MonoBehaviour
    {
        /*
         * Variables.
         */

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
        /// Input name for the blocking nose animation trigger.
        /// </summary>
        private readonly string BlockInput = "Jump";

        /// <summary>
        /// Position for the down position in the mouth.
        /// </summary>
        [SerializeField]
        private GameObject m_down;

        /// <summary>
        /// Position for the up position in the mouth.
        /// </summary>
        [SerializeField]
        private GameObject m_up;

        /// <summary>
        /// Position for the left position in the mouth.
        /// </summary>
        [SerializeField]
        private GameObject m_left;

        /// <summary>
        /// Position for the right position in the mouth.
        /// </summary>
        [SerializeField]
        private GameObject m_right;

        /// <summary>
        /// The bird's Animator component.
        /// Used when triggering differend animations.
        /// <see cref="Eat"/>
        /// <see cref="Block"/>
        /// </summary>
        private Animator m_aimator;

        /// <summary>
        /// Contains the input's name and the target position for that input.
        /// string: Input's name in Unity's Input System. (Edit/Project Settings/Input)
        /// Transform: Target position for the Input.
        /// </summary>
        private Dictionary<string, Transform> m_directions;

        /*
         * Mono Behaviour Functions.
         */

        private void Awake()
        {
            m_aimator = GetComponent<Animator>();
            //Create dictionary containing all the direction inputs and corresponding positions.
            m_directions = new Dictionary<string, Transform>()
            {
                { "Up", m_up.transform },
                { "Down", m_down.transform },
                { "Left", m_left.transform },
                { "Right", m_right.transform }
            };
        }

        private void Update()
        {
            UpdateInput();
        }

        /*
         * Functions.
         */

        /// <summary>
        /// Plays the birds eat animation.
        /// </summary>
        public void PlayEatAnimation() => m_aimator.SetTrigger(Eat);

        public void PlayBlockAnimation() => m_aimator.SetTrigger(Block);

        public void PlayUnblocAnimation() => m_aimator.SetTrigger(Unblock);

        /// <summary>
        /// Update the current inputs.
        /// </summary>
        private void UpdateInput()
        {
            //Loop through the direction inputs.
            //'set' is a key value pair, where the key is the input's name and the value is the target position.
            foreach(var set in m_directions)
            {
                if(Input.GetButtonDown(set.Key))
                {
                    MoveToPoint(set.Value);
                }
            }

            if(Input.GetButtonDown(BlockInput))
            {
                PlayBlockAnimation();
            }
            else if(Input.GetButtonUp(BlockInput))
            {
                PlayUnblocAnimation();
            }
        }

        /// <summary>
        /// Moves the Bird to a given position.
        /// </summary>
        /// <param name="target">Target position.</param>
        private void MoveToPoint(Transform target)
        {
            DOTween.Kill(transform.position);
            var time = (transform.position == target.position) ? 0 : 0.5f;
            transform.DOMove(target.transform.position, time)
                     .OnComplete(PlayEatAnimation);
        }
    }
}