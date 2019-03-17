using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Pad : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_prefab;

        [SerializeField]
        private Vector3 m_offset;

        [SerializeField]
        private KeyCode m_code;

        private SpriteRenderer m_renderer;
        private Queue<GameObject> m_notes;
        private Queue<GameObject> m_colliding;

        private void Awake()
        {
            m_notes = new Queue<GameObject>();
            m_colliding = new Queue<GameObject>();
            m_renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            UpdateInput();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.GetComponent<SpriteRenderer>().color = Color.blue;
            m_colliding.Enqueue(collision.gameObject);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            var pos = (Vector2)transform.position;
            var target = (Vector2)collision.transform.position;
            var distance = Vector2.Distance(pos, target);
            collision.GetComponent<SpriteRenderer>().color = (distance < 0.15f) ? Color.red : Color.blue;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            m_notes.Dequeue();
            m_colliding.Dequeue();
            Destroy(collision.gameObject);
        }

        public void CreateNote()
        {
            var current = Instantiate(m_prefab);
            current.transform.position = transform.position + m_offset;
            current.transform.SetParent(transform);
            m_notes.Enqueue(current);
        }

        private void UpdateInput()
        {
            if(m_colliding.Count > 0)
            {
                if(Input.GetKeyDown(Code))
                {
                    Destroy(m_colliding.Peek());
                }
            }

            m_renderer.color = Input.GetKey(Code) ? Color.gray : Color.white;
        }

        public KeyCode Code => m_code;
    }
}