using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Stores Food in a direction.
    /// Use Remove() to remove the current Food from the Tooth.
    /// Use Clear() to mark the Tooth as a free Tooth.
    /// </summary>
    public class Tooth : MonoBehaviour
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Tooth's Position in the Mouth.
        /// </summary>
        [SerializeField]
        private Direction m_direction;

        /// <summary>
        /// Current Food placed on the Tooth.
        /// </summary>
        private Food m_current;

        /*
         * Functions.
         */

        /// <summary>
        /// Places the given Food on the Tooth with the given duration.
        /// The Food lasts a bit longer than the duration.
        /// </summary>
        /// <param name="food">Food to place.</param>
        /// <param name="duration">Duration for how long the food lasts.</param>
        public void PlaceFood(Food food, float duration)
        {
            //Set the Food's game object active.
            food.gameObject.SetActive(true);
            //Set the Food's position to the Tooth's position.
            food.transform.position = transform.position;
            //Rotate the Food to face the camera.
            food.transform.LookAt(Camera.main.transform);
            //Set the current Food.
            m_current = food;
            //Initialize the current Food.
            m_current.Initialize(this, duration);
            //Mark the Food as taken.
            HasFood = true;
        }

        /// <summary>
        /// Sends the Mouth a remove request for the Tooth's direction.
        /// </summary>
        public void Remove() => Mouth.Remove(m_direction);

        /// <summary>
        /// Return the current Food object while clearing the Tooth.
        /// </summary>
        /// <returns>Current Food.</returns>
        public Food Clear()
        {
            HasFood = false;
            m_current.gameObject.SetActive(false);
            return m_current;
        }

        /*
         * Accessors.
         */

        /// <summary>
        /// Is there Food on this Tooth.
        /// Use Remove() to remove the current Food from the Tooth.
        /// Use Clear() to mark the Tooth as a free Tooth.
        /// </summary>
        /// <value>Is there Food on the Tooth.</value>
        public bool HasFood { get; private set; }

        /// <summary>
        /// Tooth's Position in the Mouth.
        /// </summary>
        /// <value>The direction in the Mouth.</value>
        public Direction Direction => m_direction;

        /// <summary>
        /// Current Mouth Instance.
        /// </summary>
        /// <value>Current Mouth Instance.</value>
        private Mouth Mouth => Mouth.Instance;
    }
}