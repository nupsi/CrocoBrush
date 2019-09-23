using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Bird Input Manager to move Bird inside Crocodile's Mouth.
    /// TODO: Test touch input.
    /// </summary>
    public class BirdInput : DirectionInputReader
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Input name for the blocking nose animation trigger.
        /// </summary>
        private readonly string BlockInput = "Jump";

        /*
         * Mono Behaviour Functions.
         */

        protected override void Update()
        {
            base.Update();
            UpdateTouch();
        }

        /*
         * Functions.
         */

        /// <summary>
        /// Update the current inputs.
        /// </summary>
        protected override void UpdateInput()
        {
            base.UpdateInput();

            if(Input.GetButtonDown(BlockInput))
            {
                Bird.PlayBlockAnimation();
            }
            else if(Input.GetButtonUp(BlockInput))
            {
                Bird.PlayUnblocAnimation();
            }
        }

        protected override void PressDirection(Direction direction) => Bird.EatAt(Bird.GetPosition(direction));

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
                    Bird.EatAt(Bird.GetPosition(ScreenPointToDirection(touch.position)));
                }
            }
#else
            //If the user clicks left mouse button.
            if(Input.GetMouseButtonDown(0))
            {
                //Move the model to the current target direction.
                Bird.EatAt(Bird.GetPosition(ScreenPointToDirection(Input.mousePosition)));
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

        /*
         * Accessors.
         */

        /// <summary>
        /// Current Bird instance.
        /// </summary>
        private Bird Bird => Bird.Instance;
    }
}