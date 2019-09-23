using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(Activator))]
    public class MenuInput : MonoBehaviour
    {
        private Activator m_activator;

        protected void Awake()
        {
            m_activator = GetComponent<Activator>();
        }

        protected void Update()
        {
            if(Input.GetButtonDown("Cancel"))
            {
                m_activator.Activate();
            }
        }
    }
}