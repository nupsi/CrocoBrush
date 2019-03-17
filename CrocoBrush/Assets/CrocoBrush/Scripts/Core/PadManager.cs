using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    public class PadManager : MonoBehaviour
    {
        private Dictionary<KeyCode, Pad> m_pads;

        private void Awake()
        {
            InitializePads();
        }

        private void InitializePads()
        {
            var pads = GetComponentsInChildren<Pad>();
            m_pads = new Dictionary<KeyCode, Pad>();
            foreach(var pad in pads)
            {
                m_pads.Add(pad.Code, pad);
            }
        }

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                CreateNode(KeyCode.UpArrow);
            }
        }

        public void Up()
        {
            CreateNode(KeyCode.UpArrow);
        }

        public void Left()
        {
            CreateNode(KeyCode.LeftArrow);
        }

        public void Right()
        {
            CreateNode(KeyCode.RightArrow);
        }

        public void CreateNode(KeyCode code)
        {
            m_pads[code]?.CreateNote();
        }
    }
}