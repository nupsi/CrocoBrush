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
        private Dictionary<Direction, Queue<GameObject>> m_notes;

        private void Awake()
        {
            m_notes = new Dictionary<Direction, Queue<GameObject>>();
            InitializeTeeth();
        }

        private void InitializeTeeth()
        {
            m_teeths = new Dictionary<Direction, List<Teeth>>();
            var teeths = GetComponentsInChildren<Teeth>();
            foreach (var teeth in teeths)
            {
                if (!m_teeths.ContainsKey(teeth.Direction))
                {
                    m_teeths.Add(teeth.Direction, new List<Teeth> { teeth });
                    m_notes.Add(teeth.Direction, new Queue<GameObject>());
                }
                else
                {
                    m_teeths[teeth.Direction].Add(teeth);
                }
            }
        }

        private void Create(Direction direction)
        {
            var target = m_teeths[direction][0].transform;
            var current = Instantiate(m_prefab);
            current.GetComponent<Food>().Initialize(this, direction, m_time, target.position);
            current.transform.SetParent(transform);
            m_notes[direction].Enqueue(current);
        }

        public void Remove(Food food)
        {
            Destroy(food.gameObject);
        }

        public void Up()
        {
            Create(Direction.Up);
        }

        public void Down()
        {
            Create(Direction.Down);
        }

        public void Left()
        {
            Create(Direction.Left);
        }

        public void Right()
        {
            Create(Direction.Right);
        }
    }
}