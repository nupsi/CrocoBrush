using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    public class Mouth : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_prefab;

        [SerializeField]
        private float m_time = 1.3f;

        private Dictionary<Direction, List<Teeth>> m_teeths;
        private Dictionary<Direction, Queue<Food>> m_notes;

        private void Awake()
        {
            m_notes = new Dictionary<Direction, Queue<Food>>();
            InitializeTeeth();
        }

        private void InitializeTeeth()
        {
            m_teeths = new Dictionary<Direction, List<Teeth>>();
            var teeths = GetComponentsInChildren<Teeth>();
            foreach(var teeth in teeths)
            {
                teeth.Initialize(this);
                if(!m_teeths.ContainsKey(teeth.Direction))
                {
                    m_teeths.Add(teeth.Direction, new List<Teeth> { teeth });
                    m_notes.Add(teeth.Direction, new Queue<Food>());
                }
                else
                {
                    m_teeths[teeth.Direction].Add(teeth);
                }
            }
        }

        public void Remove(Direction direction, Food food)
        {
            food.Clear();
            m_notes[direction].Dequeue();
            Destroy(food.gameObject);
        }

        public void PressDirection(Direction direction)
        {
            if(m_notes[direction].Count <= 0)
            {
                print("No food to clean");
            }
            else
            {
                print("There is food to clean (" + m_notes[direction].Count + ")");
                Remove(direction, m_notes[direction].Peek());
            }
        }

        public void None()
        {
        }

        public void Up() => Create(Direction.Up);

        public void Down() => Create(Direction.Down);

        public void Left() => Create(Direction.Left);

        public void Right() => Create(Direction.Right);

        private void Create(Direction direction)
        {
            var index = GetFreeTeeth(direction);
            if(index < 0)
            {
                Debug.LogError("No room to place food!");
                return;
            }
            var current = Instantiate(m_prefab);
            m_notes[direction].Enqueue(current.GetComponent<Food>());
            m_teeths[direction][index].PlaceFood(current, m_time);
        }

        private int GetFreeTeeth(Direction direction)
        {
            var teeths = m_teeths[direction];
            var index = -1;
            var space = 0;
            teeths.ForEach((teeth) =>
            {
                if(!teeth.HasFood)
                {
                    space++;
                }
            });
            if(space > 0)
            {
                do
                {
                    index = Random.Range(0, teeths.Count);
                }
                while(teeths[index].HasFood);
            }
            return index;
        }
    }
}