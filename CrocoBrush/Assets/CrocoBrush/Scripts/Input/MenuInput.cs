using UnityEngine;

namespace CrocoBrush
{
    [RequireComponent(typeof(Activator))]
    public class MenuInput : MonoBehaviour
    {
        private Activator m_activator;

        private void Awake()
        {
            m_activator = GetComponent<Activator>();
        }

        private void Update()
        {
            if(Input.GetButtonDown("Cancel"))
            {
                m_activator.Activate();
            }
        }
    }
}