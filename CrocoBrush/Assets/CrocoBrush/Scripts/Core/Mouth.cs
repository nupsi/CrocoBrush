using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Controls Teeth.
    /// Allows to add and remove food.
    /// Use Create(Direction) to add Food.
    /// Use PressDirection(Direction) to remove Food.
    /// </summary>
    public class Mouth : MonoBehaviour, ICreator
    {
        /*
         * Variables.
         */

        /// <summary>
        /// Mouth Instance.
        /// The instance is set on Awake.
        /// If you need to initialize something that requires this Instance,
        /// use Start() or setup custom srcipt execution order.
        /// </summary>
        public static Mouth Instance;

        /// <summary>
        /// Food prefab.
        /// </summary>
        [SerializeField]
        private GameObject m_prefab;

        /// <summary>
        /// Dictionary to store the teeth based on their direction.
        /// </summary>
        private Dictionary<Direction, List<Tooth>> m_teeth;

        /// <summary>
        /// Dictionary to store current Food based on their parent tooth direction.
        /// </summary>
        private Dictionary<Direction, List<Tooth>> m_notes;

        /// <summary>
        /// Queue for pooling available Food.
        /// </summary>
        private Queue<Food> m_available;

        /*
         * Mono Behaviour Functions.
         */

        private void Awake()
        {
            //Make sure there is only one Instance.
            if(Instance != null)
            {
                Debug.LogError("Multiple Mouth Instances", this.gameObject);
                return;
            }
            //set the instance.
            Instance = this;
            //Cache the Tooth components from child objects.
            InitializeTeeth();
            //Create object pool for the Food objects.
            CreatePool();
        }

        /*
         * Functions.
         */

        /// <summary>
        /// Adds Food on a Tooth on the given direction.
        /// </summary>
        /// <param name="direction">Tooths Direction where the Food is placed.</param>
        public void Create(Direction direction)
        {
            //Get index for a free Tooth.
            var index = GetFreeTooth(direction);
            //Check that we got a valid index.
            if(index < 0)
            {
                Debug.LogError("No room to place food! " + Time.time);
                return;
            }
            var tooth = m_teeth[direction][index];
            //Add the parent Tooth to the active queue in the given direction.
            m_notes[direction].Add(tooth);
            //Place the food on the free Tooth.
            tooth.PlaceFood(m_available.Dequeue(), Delay);
        }

        /// <summary>
        /// Tries to clear Food from the given direction.
        /// </summary>
        /// <param name="direction">Direction to clear the Food from.</param>
        public void PressDirection(Direction direction)
        {
            //Check if there is Food in the given Direction.
            if(m_notes[direction].Count <= 0)
            {
                //Nothing to clear.
                Crocodile.Instance.AddScore(Quality.Bad);
            }
            else
            {
                //Remove the First Food in the given direction.
                Remove(direction);
            }
        }

        /// <summary>
        /// Remove first Food from given Direction.
        /// </summary>
        /// <param name="direction">Direction to clear</param>
        public void Remove(Direction direction) => Remove(m_notes[direction][0]);

        /// <summary>
        /// Remove specific Food from a given Tooth.
        /// </summary>
        /// <param name="tooth">Tooth to clear</param>
        public void Remove(Tooth tooth)
        {
            m_notes[tooth.Direction].Remove(tooth);
            ProcessFood(tooth.Clear());
        }

        /// <summary>
        /// Reset the mouth by deactivating all active components.
        /// </summary>
        public void Restart()
        {
            Instance.StopAllCoroutines();
            foreach(var key in m_notes.Keys)
            {
                if(m_notes[key].Count > 0)
                {
                    for(int i = m_notes[key].Count - 1; i >= 0; i--)
                    {
                        var food = m_notes[key][i].Clear();
                        food.gameObject.SetActive(false);
                        m_available.Enqueue(food);
                        m_notes[key].RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// Process the given food.
        /// Processing the Food adds score based on its quality and adds the food back to object pool.
        /// </summary>
        /// <param name="food">Food to process.</param>
        private void ProcessFood(Food food)
        {
            //Add score based on the Food's quality.
            Crocodile.Instance.AddScore(food.Quality);
            //Add the Food back to object pool.
            m_available.Enqueue(food);
        }

        /// <summary>
        /// Create pool for Food objects with the current prefab.
        /// </summary>
        private void CreatePool()
        {
            //Create new queue to pool the Food object.
            m_available = new Queue<Food>();
            //Create empty pool game object.
            var root = new GameObject("Pool");
            //Place the pool under the Mouth (For cleaner scene hierarchy).
            root.transform.SetParent(transform);
            //Create the pool.
            for(int i = 0; i < transform.childCount - 1; i++)
            {
                //Instantiate the prefab.
                var current = Instantiate(m_prefab);
                //Place the current object under the pool object (For cleaner scene hierarchy).
                current.transform.SetParent(root.transform);
                //Deactive the current object.
                current.SetActive(false);
                //Add the object to the pool.
                m_available.Enqueue(current.GetComponent<Food>());
            }
        }

        /// <summary>
        /// Caches Teeth components from child objects.
        /// </summary>
        private void InitializeTeeth()
        {
            //Create new dictionary for the notes.
            m_notes = new Dictionary<Direction, List<Tooth>>();
            //Create new dictionary for the teeth.
            m_teeth = new Dictionary<Direction, List<Tooth>>();
            //Get tooth components from the child objects.
            var teeth = GetComponentsInChildren<Tooth>();
            //Loop through the tooth.
            foreach(var tooth in teeth)
            {
                //Check if key for the direction exists.
                if(!m_teeth.ContainsKey(tooth.Direction))
                {
                    //Create new list for the current direction in the teeth dictionary.
                    m_teeth.Add(tooth.Direction, new List<Tooth> { tooth });
                    //Create new queue for the current direction in the notes dictionary.
                    m_notes.Add(tooth.Direction, new List<Tooth>());
                }
                else
                {
                    //Add the tooth in the teeth list in the current direction.
                    m_teeth[tooth.Direction].Add(tooth);
                }
            }
        }

        /// <summary>
        /// Return index for a free Tooth in the given direction.
        /// If there is noo free Teeth available -1 is returned.
        /// </summary>
        /// <returns>The index for a free tooth.</returns>
        /// <param name="direction">Direction where we want the Tooth from.</param>
        private int GetFreeTooth(Direction direction)
        {
            //Check if the dictionary contains a key for the given direction.
            if(!m_teeth.ContainsKey(direction)) return -1;
            //Store the current Teeth list in a temporary variable.
            var teeth = m_teeth[direction];
            //Set the index to -1. This is the 'fail' state if no new index is found.
            var index = -1;
            for(int i = 0; i < teeth.Count; i++)
            {
                if(!teeth[i].HasFood)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        /*
         * Accessors.
         */

        /// <summary>
        /// The delay between spawning Food and playing sound.
        /// </summary>
        /// <value>The delay.</value>
        public float Delay { get; set; }
    }
}