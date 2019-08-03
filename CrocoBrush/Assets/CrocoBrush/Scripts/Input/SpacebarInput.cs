using UnityEngine;

namespace CrocoBrush
{
    public class SpacebarInput : MonoBehaviour
    {
        protected void Update()
        {
            if(Input.GetButtonDown("Jump"))
            {
                if(Spacebar.Instance.Visible)
                {
                    Spacebar.Instance.ClearSpace();
                }
                else
                {
                    Crocodile.Instance.AddScore(Quality.Bad);
                }
            }
        }
    }
}