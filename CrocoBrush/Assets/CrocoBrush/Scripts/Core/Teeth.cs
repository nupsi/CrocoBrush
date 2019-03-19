using UnityEngine;

namespace CrocoBrush
{
    public class Teeth : MonoBehaviour
    {
        [SerializeField]
        private Direction m_direction;

        public Direction Direction => m_direction;
    }
}