using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Input and animation controller for the bird flying inside the crocodile's mouth.
    /// </summary>
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
        private Dictionary<string, Direction> m_directions;

        /// <summary>
        /// Bird data containing moving positions.
        /// </summary>
        private BirdData m_data;

        /*
         * Mono Behaviour Functions.
         */

        private void Awake()
        {
            m_aimator = GetComponentInChildren<Animator>();
            //Create dictionary containing all the direction inputs and corresponding positions.
            m_directions = new Dictionary<string, Direction>()
            {
                { "Up", Direction.Up },
                { "Down", Direction.Down },
                { "Left", Direction.Left },
                { "Right", Direction.Right }
            };
        }

        private void Update()
        {
            UpdateInput();
            UpdateTouch();
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
                    MoveToPoint(GetPosition(set.Value));
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
        /// Moves bird to the right position pased on click/touch position.
        /// </summary>
        private void UpdateTouch()
        {
#if UNITY_IPHONE || UNITY_ANDROID
            //Check if the use is touching the screen.
            if(Input.touchCount > 0)
            {
                //Get the first touch point.
                var touch = Input.touches[0];
                if(touch.phase == TouchPhase.Began)
                {
                    //Move the model to the current target direction.
                    MoveToPoint(GetPosition(ScreenPointToDirection(touch.position)));
                }
            }
#else
            //If the user clicks left mouse button.
            if(Input.GetMouseButtonDown(0))
            {
                //Move the model to the current target direction.
                MoveToPoint(GetPosition(ScreenPointToDirection(Input.mousePosition)));
            }
#endif
        }

        /// <summary>
        /// Convers screen point to direction.
        /// Maps the screen from rectangle to square, splits the square into four smaller squares.
        /// Smaller squares form the top right/left and bottom right/left areas.
        /// Simple x vs y comparasion is done for each of these areas to figure out the right direction.
        /// </summary>
        /// <param name="point">Screen point.</param>
        /// <returns>Direction in the screen from the given point.</returns>
        private Direction ScreenPointToDirection(Vector2 point)
        {
            // The screen is split in the way shown in the left image to form the up, down, left and right directions.
            // The right image shows the comparasion areas top left/right and bottom left/right and
            // the variables used to make the comparisation inside these areas.
            // +------+               +--+--+
            // |\    /|               |\ | /| top left     top right
            // | \  / |     up        | \|/ | x - j        i - j
            // |  \/  |               +--+--+
            // |  /\  | left  right   | /|\ |
            // | /  \ |               |/ | \| bottom left  bottom right
            // |/    \|    down       +--+--+ x - y        i - y
            // +------+

            //Calculate the aspect ratio.
            var ratio = (float)Screen.width / Screen.height;
            //Convert the mouseposition to follow the ratio.
            point.x /= ratio;
            //Convert the mouse x position from screen point to 0-1 value,
            //where 0 is bottom left and 1 is bottom right
            var x = Mathf.Clamp01(point.x / (Screen.width / ratio));
            //Convert the mouse y position from screen point to 0-1 value,
            //where 0 is bottom left and 1 is top left
            var y = Mathf.Clamp01(point.y / Screen.height);
            //Calculate the x to use, when x > 0.5f, this is so that comparing x to y is easier.
            var i = -(x - 1);
            //Calculate the y to use, when y > 0.5f, this is so that comparing x to y is easier.
            var j = -(y - 1);
            //Do the comparation.
            //x, y, i and j should be always less than 0.5,
            //since the screen square would be 0-1 resulting in the smaller square to be 0-0.5
            return !(j < x || j < i)
                ? Direction.Down
                : (j < x && j < i)
                    ? Direction.Up
                    : (y < i)
                        ? Direction.Left
                        : Direction.Right;
        }

        /// <summary>
        /// Turns given direction into a corresponding transform.
        /// </summary>
        /// <param name="direction">Direction for transform.</param>
        /// <returns>Transform for direction.</returns>
        private Transform GetPosition(Direction direction)
        {
            return m_data?.GetDirection(direction) ?? transform;
        }

        /// <summary>
        /// Moves the Bird to a given position.
        /// </summary>
        /// <param name="target">Target position.</param>
        private void MoveToPoint(Transform target)
        {
            DOTween.Kill(transform.position);
            var time = (transform.position == target.position) ? 0 : 0.5f;
            DOTween.Sequence()
                .Append(transform.DOMove(target.transform.position, time))
                .Join(transform.DORotate(target.transform.rotation.eulerAngles, time))
                .OnComplete(()=>PlayEatAnimation())
                .Play();
        }
    }
}