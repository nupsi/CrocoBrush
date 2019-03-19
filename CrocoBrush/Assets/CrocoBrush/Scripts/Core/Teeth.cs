using UnityEngine;

namespace CrocoBrush
{
    public class Teeth : MonoBehaviour
    {
        [SerializeField]
        private Direction m_direction;

        private Mouth m_mouth;
        private Food m_current;

        public void Initialize(Mouth mouth)
        {
            m_mouth = mouth;
        }

        public void PlaceFood(GameObject food, float time)
        {
            food.transform.SetParent(transform);
            food.transform.position = transform.position;
            m_current = food.GetComponent<Food>();
            m_current.Initialize(this, time);
            HasFood = true;
        }

        public void Remove()
        {
            if(m_mouth != null)
            {
                m_mouth.Remove(m_direction, m_current);
            }
        }

        public void Clear()
        {
            HasFood = false;
        }

        public bool HasFood { get; private set; }
        public Direction Direction => m_direction;
    }
}