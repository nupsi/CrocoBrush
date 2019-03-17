using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Note : MonoBehaviour
    {
        [SerializeField]
        private Vector3 m_direction;

        private bool m_colliding;
        private SpriteRenderer m_renderer;

        private void Awake()
        {
            m_renderer = GetComponent<SpriteRenderer>();
        }

        private void Reset()
        {
            var body = GetComponent<Rigidbody2D>();
            body.bodyType = RigidbodyType2D.Kinematic;
        }

        private void Update()
        {
            transform.position += m_direction * Time.deltaTime;
        }
    }
}